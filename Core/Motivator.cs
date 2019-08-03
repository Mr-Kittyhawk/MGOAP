using System.Collections.Generic;

namespace MGOAP
{
    ///<summary>This could be a job or a system of needs, motivators fight for priority and then determine tbe goal they'd like to accomplish.</summary> 
    public abstract class Motivator
    {
        public List<Idea> Goals { get; }

        public abstract int GetPriority();
        public abstract Idea GetGoal();
    }
}