using Navigation.Domain.Game.Robot.Visions;
using Navigation.Infrastructure;

namespace Navigation.Domain.Game.Robot
{
    public abstract class MobileRobot : IMobileRobot
    {
        public Point Position { get; protected set; }
        public IRobotVision RobotVision { get; }
        
        protected MobileRobot(IRobotVision robotVision, Point position)
        {
            Position = position;
            RobotVision = robotVision;
        }

        public abstract void Run();
    }
}
