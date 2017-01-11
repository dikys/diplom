using Navigation.Domain.Robot.Visions;
using Navigation.Infrastructure;

namespace Navigation.Domain.Robot
{
    public abstract class MobileRobot
    {
        public Point Position { get; protected set; }
        public Vision Vision { get; }

        public double Size { get; }

        protected MobileRobot(Maze.Maze maze, Point position)
        {
            Position = position;
            Vision = new Vision(maze, this);

            Size = 5;
        }

        public abstract void Run();
    }
}
