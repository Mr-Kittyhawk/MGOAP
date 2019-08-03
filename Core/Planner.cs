using System.Collections.Generic;

namespace MGOAP
{
    ///<summary>The planner is responsible for taking in a goal and determing a list of workable actions from it.</summary>
    class Planner
    {
        public List<Action> PotentialActionPool { get; set; }
        public int StepCost { get; set; }
        private List<Action> usableActionPool;
        private ActionGraph actionGraph;
        private ActionGraphNode finalNode;


        public Planner(List<Action> potentialActions)
        {
            PotentialActionPool = potentialActions;
            actionGraph = new ActionGraph();
            StepCost = 0;
        }

        public Plan FindPlan(Goal goal)
        {
            //if the given goal is already accomplished for some reason, exit early.
            if (goal.Test() == true)
            {
                System.Console.WriteLine("MGOAP Planner: given goal " + goal.ExpectedState + " is already true!");
                return new Plan();
            }

            FilterActions();

            actionGraph.Clear();

            return Astar(ConstructGraph(goal));
        }

        private ActionGraph ConstructGraph(Idea goal)
        {
            foreach (Action action in usableActionPool)
            {
                //if an action solves our current need add it to our graph
                if (action.Effects.Contains(goal))
                {
                    var node = new ActionGraphNode(action);
                    actionGraph.RootNodes.Add(node);
                }
            }
            foreach (ActionGraphNode node in actionGraph.RootNodes)
            {
                RecursiveGraphBuilder(node);
            }

            return actionGraph;
        }

        private void FilterActions()
        {
            usableActionPool = new List<Action>();
            foreach (Action action in PotentialActionPool)
            {
                //if an action currently has an unsolvable contextual condition don't add it to the usable pool
                foreach (Idea contextualCondition in action.ContextualConditions)
                {
                    if (contextualCondition.Evaluate())
                        usableActionPool.Add(action);
                }
            }
        }

        private void RecursiveGraphBuilder(ActionGraphNode lastNode)
        {
            foreach (Idea precondition in lastNode.Action.Preconditions)
            {
                foreach (Action action in usableActionPool)
                {
                    //if an action solves our current need add it to our graph
                    if (action.Effects.Contains(precondition))
                    {
                        var currentNode = new ActionGraphNode(action);
                        actionGraph.AddNode(lastNode, currentNode);

                        //recurse a level deeper if this new action has preconditions to solve
                        if (action.Preconditions != null)
                        {
                            RecursiveGraphBuilder(currentNode);
                        }
                    }
                }
            }
        }

        private Plan Astar(ActionGraph graph)
        {
            var plan = new Plan();
            var walkableNodes = graph.RootNodes;

            var root = new ActionGraphNode(null, null);

            Search(root);

            //assemble the plan
            plan.Actions.Push(finalNode.Action);
            var node = finalNode.Parent;
            while (node.Parent != null)
            {
                plan.Actions.Push(node.Action);
            }

            return plan;
        }

        private void Search(ActionGraphNode currentNode)
        {
            currentNode.Closed = true;
            var nextNodes = GetWalkableNodes(currentNode);
            nextNodes.Sort((node1, node2) => node1.Action.CostValue.CompareTo(node2.Action.CostValue));
            foreach (var nextNode in nextNodes)
            {
                if (nextNode.Action.Preconditions.Count == 0)
                    finalNode = nextNode;
                else
                    Search(nextNode);
            }
        }

        private List<ActionGraphNode> GetWalkableNodes(ActionGraphNode fromNode)
        {
            var walkableNodes = new List<ActionGraphNode>();

            foreach (var child in fromNode.Children)
            {
                // Ignore already-closed nodes
                if (child.Closed) continue;

                // Already-open nodes are only added to the list if their cost value is lower going via this route
                else if (!child.Closed)
                {
                    if ((fromNode.Action.CostValue + StepCost) < child.Action.CostValue)
                        walkableNodes.Add(child);
                }
                else
                {
                    // If it's untested, set the parent and flag it as open for consideration
                    child.Closed = false;
                    walkableNodes.Add(child);
                }
            }
            return walkableNodes;
        }
    }
}