using System.Collections.Generic;
using System.Text;

/// <summary> Motivated Goal Oriented Action Planning. </summary>
namespace MGOAP
{
    /// <summary>
    /// A Condition is an object that knows how to go out and determine if something is true or false.
    /// It knows where some piece of data lives, and the logic for evaluating that data.
    /// </summary> 
    public abstract class Condition
    {
        public abstract bool Evaluate();

        /// <summary>Name of the Condition</summary>
        public override string ToString() => GetType().Name;
    }

    /// <summary>An Idea is a <see cref="Condition"/> that we want or expect to have a specific outcome.</summary>
    public class Idea
    {
        private Condition condition;
        private bool expectedState;

        public Idea(Condition condition, bool state)
        {
            this.condition = condition;
            expectedState = state;
        }

        /// <summary>Does our expectedState match the state of our condition?</summary>
        public bool Evaluate() => condition.Evaluate() == expectedState ? true : false;


        /// <summary>Name of the condition + the expected state it should evaluate to.</summary>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(condition.GetType().Name);
            builder.Append(": ");
            builder.Append(expectedState);
            return builder.ToString();
        }
    }

    /// <summary>A Plan is a series of actions that can be followed to attempt to accomplish a goal.</summary>
    public struct Plan
    {
        public Stack<Action> Actions { get; private set; }

        public Plan(Stack<Action> actions) => Actions = actions;
        public Plan(Action action)
        {
            Actions = new Stack<Action>();
            Actions.Push(action);
        }

        /// <summary></summary>
        public override string ToString()
        {
            var builder = new StringBuilder();

            foreach (var action in Actions)
            {
                builder.Append(action.GetType().Name);
                builder.Append(": ");
            }

            return builder.ToString();
        }
    }

    /// <summary>A Goal is a number of requirements that need to be fufilled in order to be completed.</summary>
    public class Goal
    {
        public Idea[] Requirements { get; private set; }

        public Goal(Idea[] requirements) => Requirements = requirements;
        public Goal(Idea requirement)
        {
            Requirements = new Idea[] { requirement };
        }

        /// <summary>Determine if this goal is accomplished.</summary>
        public bool Completed()
        {
            for (int i = 0; i < Requirements.Length; i++)
            {
                if (Requirements[i].Evaluate() == false)
                    return false;
            }
            return true;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(GetType().Name);
            builder.Append("");
        }
    }
}