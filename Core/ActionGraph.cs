using System.Collections.Generic;

namespace MGOAP
{
    /// <summary>
    /// An action graph contains all possible <see cref="Plan"/>s that can be used to solve a goal.
    /// </summary>
    internal class ActionGraph
    {
        internal List<ActionGraphNode> RootNodes { get; set; }
        internal Action SelectedAction { get; }

        internal ActionGraph()
        {
            RootNodes = new List<ActionGraphNode>();
        }

        internal ActionGraph(List<Action> rootActions)
        {
            RootNodes = new List<ActionGraphNode>();
            foreach (Action action in rootActions)
                RootNodes.Add(new ActionGraphNode(action));
        }

        internal void AddNode(ActionGraphNode parent, ActionGraphNode child)
        {
            parent.Children.Add(child);
        }

        internal void RemoveNode(ActionGraphNode node)
        {
            foreach (ActionGraphNode child in node.Children)
                child.Parent = null;

            node.Parent.Children.Remove(node);
        }

        internal void Clear()
        {
            RootNodes.Clear();
        }
    }

    internal class ActionGraphNode
    {
        internal ActionGraphNode Parent { get; set; }
        internal List<ActionGraphNode> Children { get; set; }
        internal Action Action { get; set; }
        internal bool IsLeaf { get { return Children.Count == 0 ? true : false; } }
        internal bool Closed { get; set; } //this is for AStar

        internal int Depth
        {
            get
            {
                int depth = 0;
                ActionGraphNode selection = this;
                while (selection.Parent != null)
                {
                    selection = selection.Parent;
                    depth++;
                }
                return depth;
            }
        }

        internal ActionGraphNode Root
        {
            get
            {
                ActionGraphNode selection = this;
                while (selection.Parent != null)
                    selection = selection.Parent;
                return selection;
            }
        }

        internal ActionGraphNode(ActionGraphNode parent, Action action)
        {
            Parent = parent;
            Action = action;
            Children = new List<ActionGraphNode>();
        }

        internal ActionGraphNode(Action action)
        {
            Parent = null;
            Action = action;
            Children = new List<ActionGraphNode>();
        }
    }
}