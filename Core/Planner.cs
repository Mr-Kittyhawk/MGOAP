using System.Collections.Generic;
using System.Linq;

namespace MGOAP {
    ///<summary> The <see cref="Planner"/> is responsible for taking in a <see cref="Goal"/> and creating a workable <see cref="Plan"/> from it. </summary>
    class Planner {

        /// <summary> All <see cref="Action"/>s this <see cref="Planner"/> has to work with. </summary>
        public List<Action> PotentialActionPool { get; set; }
        /// <summary> <see cref="Action"/>s that have no unfufilled contextual conditions. </summary>
        private List<Action> usableActionPool;


        private List<ActionGraph> actionGraphs;
        private ActionGraph.Node finalNode;

        // for A* search
        private List<ActionGraph.Node> openNodes;
        private List<ActionGraph.Node> closedNodes;

        #region Construction
        public Planner(List<Action> potentialActions) {
            PotentialActionPool = potentialActions;
            actionGraphs = new List<ActionGraph>();
            openNodes = new List<ActionGraph.Node>();
            closedNodes = new List<ActionGraph.Node>();
        }
        #endregion Construction

        /// <summary> Determines a series of <see cref="Action"/>s that can be used to complete a given <see cref="Goal"/>. </summary>
        public Plan GeneratePlan(Goal goal) {
            var unsolvedRequirements = new List<Condition>();

            // determine what parts of our goal still need solving
            for (int i = 0; i < goal.Requirements.Length; i++) {
                if (goal.Requirements[i].Evaluate() == false)
                    unsolvedRequirements.Add(goal.Requirements[i]);
            }

            // the given goal is completely solved already, return an empty plan.
            if (unsolvedRequirements.Count == 0)
                return new Plan();

            DetermineAvailableActions();

            ConstructGraphs(unsolvedRequirements);

            return SolveGraphs();
        }

        /// <summary> Fills the <see cref="usableActionPool"/>. </summary>
        private void DetermineAvailableActions() {
            usableActionPool.Clear();

            foreach (var action in PotentialActionPool) {
                foreach (var contextualCondition in action.ContextualConditions) {
                    // if an action currently has an unsolvable contextual condition don't add it to the usable pool
                    if (contextualCondition.Evaluate() == true)
                        usableActionPool.Add(action);
                }
            }
        }

        private void ConstructGraphs(List<Condition> requirements) {
            actionGraphs.Clear();

            for (int i = 0; i < requirements.Count; i++) {
                var rootActions = new List<ActionGraph.Node>();

                foreach (Action action in usableActionPool) {
                    if (action.Effects.Contains(requirements[i]))
                        rootActions.Add(new ActionGraph.Node(action));
                }

                var graph = new ActionGraph(rootActions);

                foreach (ActionGraph.Node node in graph.RootNodes) {
                    RecursiveGraphBuilder(node);
                }

                actionGraphs.Add(graph);
            }
        }

        private void RecursiveGraphBuilder(ActionGraph.Node lastNode) {
            foreach (Condition precondition in lastNode.Action.Preconditions) {
                foreach (Action action in usableActionPool) {
                    // if an action solves our current need add it to our graph
                    if (action.Effects.Contains(precondition)) {
                        var currentNode = new ActionGraph.Node(lastNode, action);

                        // recurse a level deeper if this new action has preconditions to solve
                        if (action.Preconditions != null) {
                            RecursiveGraphBuilder(currentNode);
                        }
                    }
                }
            }
        }

        private Plan SolveGraphs() {
            var plan = new Plan();

            foreach (var graph in actionGraphs) {
                var nullNode = new ActionGraph.Node(null);
                nullNode.Children = graph.RootNodes;
                openNodes.Add(nullNode);
                ActionGraph.Node currentNode = null;
                int currentIndex = 0;

                while (openNodes.Count != 0) {
                    // set current node to the lowest cost node
                    for (int i = 0; i < openNodes.Count; i++) {
                        if (openNodes[i].Cost + openNodes[i].PathCost < currentNode.PathCost + currentNode.Cost) {
                            currentNode = openNodes[i];
                            currentIndex = i;
                        }
                    }

                    currentNode = openNodes[currentIndex];
                    openNodes.RemoveAt(currentIndex);
                    closedNodes.Add(currentNode);

                    // if we've found a solution we're done
                    if (currentNode.IsLeaf()) {
                        finalNode = currentNode;
                        break;
                    }

                    else // process the nodes
                    {
                        foreach (var child in currentNode.Children) {
                            if (closedNodes.Contains(child))
                                continue;

                            if (openNodes.Contains(child)) {
                                if (child.Cost > currentNode.Cost)
                                    continue;
                                else
                                    openNodes.Add(child);
                            }
                        }
                    }
                }

                // append our found path onto the plan
                while (currentNode.IsRoot() == false) {
                    plan.Actions.Push(currentNode.Action);
                    currentNode = currentNode.Parent;
                }
            }

            return plan;
        }
    }
}
