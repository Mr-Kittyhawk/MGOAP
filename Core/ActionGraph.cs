using System.Collections.Generic;

namespace MGOAP
{
    /// <summary>
    /// An action graph contains all possible <see cref="Plan"/>s that can be used to solve a goal.
    /// </summary>
    internal sealed class ActionGraph
    {
        internal List<Node> RootNodes { get; set; }
        internal Action SelectedAction { get; }

        internal ActionGraph(List<Node> rootNodes)
        {
            RootNodes = rootNodes;
        }

        internal void AddNode(Node parent, Node child)
        {
            parent.Children.Add(child);
            child.Parent = parent;
            child.UpdatePathCost();
        }

        internal void RemoveNode(Node node)
        {
            for (int i = 0; i < node.Children.Count; i++)
                node.Children[i].Parent = null;

            node.Parent.Children.Remove(node);
        }

        internal class Node
        {
            internal Node Parent { get; set; }
            internal List<Node> Children { get; set; }
            internal Action Action { get; private set; }

            internal int Cost; // G in the A* algorithm

            internal int PathCost; // H in the A* algorithm

            internal Node(Node parent, Action action)
            {
                Parent = parent;
                Action = action;
                Children = new List<Node>();
                Cost = action.GetCost();
                UpdatePathCost();
            }

            internal Node(Action action)
            {
                Parent = null;
                Action = action;
                Children = new List<Node>();
                Cost = action.GetCost();
            }

            internal int GetDepth()
            {
                Node selection = this;
                int depth = 0;
                while (selection.Parent != null)
                {
                    selection = selection.Parent;
                    depth++;
                }
                return depth;
            }

            internal void UpdatePathCost()
            {
                int cost = 0;
                Node selection = this;

                while(selection.Parent != null)
                {
                    cost += selection.Parent.Cost;
                    selection = selection.Parent;
                }

                PathCost = cost;
            }

            internal bool IsLeaf() => Children.Count == 0 ? true : false;
            internal bool IsRoot() => Parent == null ? true : false;
        }
    }
}