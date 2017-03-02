using System.Drawing;
using Navigation.App.Windows.Controls;
using Navigation.Domain.Robot;
using Navigation.Domain.Strategies.DFS;
using Navigation.UI.Windows.Controls;
using Point = Navigation.Infrastructure.Point;

namespace Navigation.UI
{
    public class GameViewer
    {
        public MazeViewer MazeViewer;

        private MobileRobot _robot;
        private Canvas _canvas;

        public GameViewer()
        {
            var maze = MazeViewer.GetDefaultMaze();

            MazeViewer = new MazeViewer(maze);
            //_robot = new RobotWithDFS(maze, new Point(50, 50));
        }

        public void RunRobot()
        {
            //_robot = new RobotWithDFS(MazeViewer.DefaultMaze, new Point(50, 50));

            _robot.Run();
        }

        public void Draw(Canvas canvas)
        {
            MazeViewer.Draw(canvas);

            canvas.Draw(Brushes.LawnGreen, _robot.Position);
        }
    }
}
