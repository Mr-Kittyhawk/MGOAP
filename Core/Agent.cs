using System.Collections.Generic;

namespace MGOAP {

    /// <summary> An individual AI. </summary>
    public sealed class Agent {

        public Goal Goal { get; private set; }
        public Plan Plan { get; private set; }

        public List<Motivator> MotivatorPool { get; set; }
        public List<Action> ActionPool { get => planner.PotentialActionPool; set => planner.PotentialActionPool = value; }


        private Planner planner;

        #region Construction
        public Agent() {
            planner = new Planner(new List<Action>());
            MotivatorPool = new List<Motivator>();
        }

        public Agent(List<Action> actions, List<Motivator> motivations) {
            planner = new Planner(actions);
            MotivatorPool = motivations;
        }
        #endregion Construction

        public void Start() {
            DetermineGoal();
        }

        public void Stop() {

        }

        public void DetermineGoal() {
            // determine which motivation has the highest priority
            var priorityMotivator = MotivatorPool[0];
            var highestpriority = priorityMotivator.GetPriority();
            for (int i = 1; i < MotivatorPool.Count; i++) {
                if (MotivatorPool[i].GetPriority() > highestpriority) {
                    priorityMotivator = MotivatorPool[i];
                    highestpriority = priorityMotivator.GetPriority();
                }
            }

            Goal = priorityMotivator.GetGoal();
            DeterminePlan();
        }

        public void DeterminePlan() {
            Plan = planner.GeneratePlan(Goal);

            //if we're already at the location we need to be to perform an action just do it
            if (Plan.Actions.Peek().InRange())
                PerformAction();
            else
                MoveTo();
        }

        void MoveTo() {
            //Vector3 target = Plan.Actions.Peek().PerformLocation();

            //var path = nav.GetSimplePath(base.Transform.origin, target);

            //for (int i = 0; i < path.Length; i++)
            //{

            //}

            PerformAction();
        }

        void PerformAction() {
            Plan.Actions.Pop().PerformAction();

            //play the animation for the action if there is one

            //adjust cost value?

            //if we have more actions in the plan to run move to their location and run them.
            if (Plan.Actions.Count != 0)
                MoveTo();
            else
                DetermineGoal();
        }
    }
}
