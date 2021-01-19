using StringBuilder = System.Text.StringBuilder;

namespace MGOAP {

    /// <summary> A bundle of logic for changing some amount of world state outside of the AI. </summary>
    public abstract class Action {

        /// <summary> <see cref="Condition"/>s that should be set to true by running <see cref="PerformAction"/>. </summary>
        public Condition[] Effects { get; private set; }

        /// <summary>
        /// <see cref="Condition"/>s that need to be true in order for <see cref="PerformAction"/> to run sucessfully. <br/>
        /// If these conditions evaluate to false, the <see cref="Planner"/> will try to solve them by adding additional actions to the graph.
        /// </summary>
        public Condition[] Preconditions { get; private set; }

        /// <summary>
        /// <see cref="Condition"/>s that need to be true in order for <see cref="PerformAction"/> to run sucessfully. <br/>
        /// The <see cref="Planner"/> will NOT attempt to solve these conditions using additional actions.
        /// </summary>
        public Condition[] ContextualConditions { get; private set; }


        #region Construction
        public Action(Condition effect, Condition precondition = null, Condition contextualCondition = null) {
            Effects = new Condition[] { effect };
            Preconditions = new Condition[] { precondition };
            ContextualConditions = new Condition[] { contextualCondition };
        }

        public Action(Condition[] effects, Condition[] preconditions, Condition[] contextualConditions) {
            Effects = effects;
            Preconditions = preconditions;
            ContextualConditions = contextualConditions;
        }
        #endregion 

        public abstract void PerformAction();

        /// <summary> Can we perform the action at our current position? </summary>
        public abstract bool InRange();

        // public abstract Vector3 PerformLocation(); //the location the agent needs to move to to perform the action

        /// <summary> Name of the Action + CostValue. </summary>
        public override string ToString() {
            var builder = new StringBuilder();
            builder.Append(GetType().Name);
            builder.Append(": ");
            builder.Append(GetCost());
            return builder.ToString();
        }

        /// <summary> Hueristic to control how likely the MGOAP <see cref="Agent"/> is to use this action. </summary>
        public abstract int GetCost();
    }
}