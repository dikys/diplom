using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigation.Domain.Maze;
using Navigation.Domain.Robot;
using Navigation.Domain.Robot.DFS;
using Navigation.Infrastructure;
using Point = Navigation.Infrastructure.Point;

namespace Navigation.App
{
    public class GameViewer
    {
        public Line MazeDiameter => _maze.Diameter;

        private Maze _maze;
        private MobileRobot _robot;

        private Canvas _canvas;

        public GameViewer(Canvas canvas)
        {
            _maze = GetDefaultMaze();
            _robot = new RobotWithDfs(_maze, new Point(50, 50));
            _canvas = canvas;

            canvas.Paint += (sender, args) =>
            {
                var g = args.Graphics;

                foreach (var wall in _maze.Walls)
                {
                    canvas.Draw(wall);
                }

                /*(_robot as RobotWithDfs).ViewedContours.ForEach(
                    contour =>
                        g.FillPolygon(Brushes.AliceBlue,
                            contour.SelectMany(line => new PointF[] {line.Start.ToPointF(), line.End.ToPointF()})
                                .ToArray()));*/

                (_robot as RobotWithDfs).CurrentNode.AdjacentNodes.ForEach(
                    node =>
                        _canvas.Draw(Brushes.Red, node.Position));

                (_robot as RobotWithDfs).CurrentNode.NotDeadLockAdjacentNodes.ToList().ForEach(
                    node =>
                        _canvas.Draw(node.Position));

                canvas.Draw(Brushes.LawnGreen, _robot.Position);
            };
        }

        public void RunRobot()
        {
            _robot.Run();

            _canvas.Invalidate();
        }

        private Maze GetDefaultMaze()
        {
            return new Maze(new Wall[]{
                new Wall(new Line(50, 25, 75, 25)),
                new Wall(new Line(75, 25, 100, 50)),
                new Wall(new Line(100, 50, 100, 75)),
                new Wall(new Line(100, 75, 75, 100)),
                new Wall(new Line(75, 100, 50, 100)),
                new Wall(new Line(50, 100, 25, 75)),
                new Wall(new Line(25, 75, 25, 50)),
                new Wall(new Line(25, 50, 50, 25)),
                
                new Wall(new Line(75, 50, 75, 75)),
                new Wall(new Line(75, 75, 50, 75)),
                new Wall(new Line(50, 75, 75, 50))         
            });
        }
    }
}
