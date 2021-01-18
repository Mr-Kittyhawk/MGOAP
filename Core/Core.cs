using System.Collections.Generic;
using System.Text;

/// <summary> Motivated Goal Oriented Action Planning. </summary>
namespace MGOAP {
    /// <summary>
    /// A Condition is an object that knows how to go out and determine if something is true or false. <br/>
    /// It knows where some piece of data lives, and the logic for evaluating that data.
    /// </summary> 
    public abstract class Condition {
        public abstract bool Evaluate();

        /// <summary> Name of the Condition. </summary>
        public override string ToString() => GetType().Name;
    }

    /// <summary> A Plan is a series of actions that can be followed to attempt to accomplish a goal. </summary>
    public struct Plan {
        public Stack<Action> Actions { get; private set; }

        public Plan(Stack<Action> actions) => Actions = actions;
        public Plan(Action action) {
            Actions = new Stack<Action>();
            Actions.Push(action);
        }

        public override string ToString() {
            var builder = new StringBuilder();

            foreach (var action in Actions) {
                builder.Append(action.GetType().Name);
                builder.Append(": ");
            }

            return builder.ToString();
        }
    }

    /// <summary> A Goal is a number of requirements that need to be fufilled in order to be completed. </summary>
    public class Goal {
        public Condition[] Requirements { get; private set; }

        public Goal(Condition[] requirements) => Requirements = requirements;
        public Goal(Condition requirement) {
            Requirements = new Condition[] { requirement };
        }

        /// <summary> Determine if this goal is accomplished. </summary>
        public bool Completed() {
            for (int i = 0; i < Requirements.Length; i++) {
                if (Requirements[i].Evaluate() == false)
                    return false;
            }
            return true;
        }

        public override string ToString() {
            var builder = new StringBuilder();
            builder.Append(GetType().Name);
            builder.Append("");
            return builder.ToString();
        }
    }
}