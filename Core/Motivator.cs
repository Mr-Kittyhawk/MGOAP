
namespace MGOAP {
    ///<summary>
    ///Determines what <see cref="Goal"/> an <see cref="Agent"/> would like to accomplish. <br/>
    ///<see cref="Motivator"/>s compete with eachother for attention using the <see cref="GetPriority"/> heuristic. 
    ///</summary> 
    public abstract class Motivator {
        public abstract int GetPriority();
        public abstract Goal GetGoal();
    }
}
