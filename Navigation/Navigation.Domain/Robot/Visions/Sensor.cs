using Navigation.Domain.Maze;
using Navigation.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigation.Domain.Robot.Visions
{
    public class Sensor
    {
        public double Angle;

        public Sensor(Maze.Maze maze, MobileRobot robot, double rotationAngle)
        {
            _maze = maze;
            _robot = robot;
            _rotationAngle = rotationAngle;
            _rayLength = 1.1 * maze.Diameter.Length;

            Reset();
        }

        private readonly Maze.Maze _maze;
        private readonly MobileRobot _robot;
        private readonly double _rotationAngle;
        private readonly double _rayLength;
        private Line _ray;
        
        public void Rotate()
        {
            _ray = _ray.Rotate(_rotationAngle);

            Angle += _rotationAngle;
        }
        
        public bool LookForward(ref Point observedPoint, ref Wall observedWall)
        {
            var haveGap = true;
            
            // вспомогательные переменные
            var distanceToObservedPoint = 0.0;
            var currentIntersectionPoint = new Point();
            // конец в.п.

            foreach (var wall in _maze.Walls)
            {
                if (!_ray.HaveIntersectionPoint(wall.Line, ref currentIntersectionPoint))
                    continue;

                if (haveGap)
                {
                    haveGap = false;

                    observedWall = wall;
                    observedPoint = currentIntersectionPoint;
                    distanceToObservedPoint = _robot.Position.GetDistanceTo(observedPoint);

                    continue;
                }

                if (_robot.Position.GetDistanceTo(currentIntersectionPoint) < distanceToObservedPoint)
                {
                    observedWall = wall;
                    observedPoint = currentIntersectionPoint;
                    distanceToObservedPoint = _robot.Position.GetDistanceTo(observedPoint);
                }
            }

            if (haveGap)
                throw new InvalidOperationException("Border of maze has gap");

            return observedWall.IsFinish;
        }

        public void Reset()
        {
            _ray = new Line(_robot.Position, _robot.Position + new Point(_rayLength, 0));

            Angle = 0;
        }
    }
}
