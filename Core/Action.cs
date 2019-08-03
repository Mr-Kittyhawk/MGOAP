using StringBuilder = System.Text.StringBuilder;

namespace MGOAP
{
    /// <summary>
    /// The main building blocks of MGOAP, an action does several things.
    /// 
    /// </summary>
    public abstract class Action
    {
        /// <summary>Hueristic to control how likely the MGOAP <see cref="Agent"/> is to use this action.</summary>
        public int CostValue { get; set; }

        /// <summary>What the <see cref="Planner"/> knows this action should change in world state.</summary>
        public Idea[] Effects { get; private set; } 
        
        /// <summary>
        /// Conditions that need to be true in order to complete the action.
        /// If these conditions evaluate to false, the <see cref="Planner"/> will try to
        /// </summary>
        public Idea[] Preconditions { get; private set; }

        /// <summary>
        /// Conditions that need to be true in order to complete the action.
        /// The <see cref="Planner"/> will NOT attempt to solve these conditions using additional actions.
        /// </summary>
        public Idea[] ContextualConditions { get; private set; }


        #region constructors
        public Action(Idea effect, Idea precondition, Idea contextualCondition, int costValue)
        {
            Effects = new Idea[] { effect };
            Preconditions = new Idea[] { precondition };
            ContextualConditions = new Idea[] { contextualCondition };
            CostValue = costValue;
        }

        public Action(Idea[] effects, Idea[] preconditions, Idea[] contextualConditions, int costValue)
        {
            Effects = effects;
            Preconditions = preconditions;
            ContextualConditions = contextualConditions;
            CostValue = costValue;
        }
        #endregion 

        public abstract void PreformAction();

        public abstract bool InRange(); //can we preform the action at our current position?

        public abstract Vector3 PreformLocation(); //the location the agent needs to move to to preform the action

        /// <summary>Name of the Action + CostValue.</summary>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(GetType().Name);
            builder.Append(": ");
            builder.Append(CostValue);
            return builder.ToString();
        }
    }
}