using StringBuilder = System.Text.StringBuilder;

namespace MGOAP
{
    /// <summary>
    /// The main building blocks of MGOAP, an action does several things.
    /// 
    /// </summary>
    public abstract class Action
    {
        /// <summary>What the <see cref="Planner"/> knows this action should change in world state.</summary>
        public Condition[] Effects { get; private set; }

        /// <summary>
        /// Conditions that need to be true in order to complete the action.
        /// If these conditions evaluate to false, the <see cref="Planner"/> will try to solve them by adding additional
        /// actions to the graph.
        /// </summary>
        public Condition[] Preconditions { get; private set; }

        /// <summary>
        /// Conditions that need to be true in order to complete the action.
        /// The <see cref="Planner"/> will NOT attempt to solve these conditions using additional actions.
        /// </summary>
        public Condition[] ContextualConditions { get; private set; }


        #region constructors
        public Action(Condition effect, Condition precondition = null, Condition contextualCondition = null)
        {
            Effects = new Condition[] { effect };
            Preconditions = new Condition[] { precondition };
            ContextualConditions = new Condition[] { contextualCondition };
        }

        public Action(Condition[] effects, Condition[] preconditions, Condition[] contextualConditions)
        {
            Effects = effects;
            Preconditions = preconditions;
            ContextualConditions = contextualConditions;
        }
        #endregion 

        public abstract void PreformAction();

        public abstract bool InRange(); //can we preform the action at our current position?

        // public abstract Vector3 PreformLocation(); //the location the agent needs to move to to preform the action

        /// <summary>Name of the Action + CostValue.</summary>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(GetType().Name);
            builder.Append(": ");
            builder.Append(GetCost());
            return builder.ToString();
        }

        /// <summary>Hueristic to control how likely the MGOAP <see cref="Agent"/> is to use this action.</summary>
        public abstract int GetCost();
    }
}