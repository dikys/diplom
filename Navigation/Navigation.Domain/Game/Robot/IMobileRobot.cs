using Navigation.Domain.Game.Robot.Visions;
using Navigation.Infrastructure;

namespace Navigation.Domain.Game.Robot
{
    public interface IMobileRobot
    {
        Point Position { get; }
        IRobotVision RobotVision { get; }
        
        void Run();
    }
}
