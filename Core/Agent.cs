using System.Collections.Generic;

namespace MGOAP
{
    class Agent
    {
        public enum Status
        {
            DeterminingMotivation,
            DeterminingPlan,
            Moving,
            PreformingAction
        };

        public Status State { get; private set; }

        public List<Action> ActionPool { get; set; }

        public List<Motivator> MotivatorPool { get; set; }

        public Goal Goal { get; private set; }
        public Plan Plan { get; private set; }

        private Planner planner;
        
        public Agent()
        {
            ActionPool = new List<Action>();
            MotivatorPool = new List<Motivator>();

            planner = new Planner(ActionPool);

            DetermineGoal();
        }


        public void Tick()
        {

        }

        #region AgentStateMachine
        void DetermineGoal()
        {
            State = Status.DeterminingMotivation;

            //determine which motivation has the highest priority

            //let motivation evaluate its top goal

            //FormPlan(goal);
        }

        void FormPlan(Goal goal)
        {
            State = Status.DeterminingPlan;

            Plan = planner.FindPlan(goal);

            //if we're already at the location we need to be to preform an action just do it
            if (Plan.Actions.Peek().InRange())
                PreformAction();
            else
                MoveTo();
        }

        void MoveTo()
        {
            State = Status.Moving;
            Vector3 target = Plan.Actions.Peek().PreformLocation();

            var path = nav.GetSimplePath(base.Transform.origin, target);

            for (int i = 0; i < path.Length; i++)
            {

            }

            PreformAction();
        }

        void PreformAction()
        {
            Status = Status.PreformingAction;

            Plan.Actions.Pop().PreformAction();

            //play the animation for the action if there is one

            //adjust cost value?

            //if we have more actions in the plan to run move to their location and run them.
            if (Plan.Actions.Count != 0)
                MoveTo();
            else
                DetermineGoal();

        }
        #endregion
    }
}