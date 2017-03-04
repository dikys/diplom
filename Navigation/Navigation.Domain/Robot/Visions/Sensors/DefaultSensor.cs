using System;
using System.Linq;
using Navigation.Domain.Exceptions;
using Navigation.Domain.Mazes;
using Navigation.Infrastructure;

namespace Navigation.Domain.Robot.Visions.Sensors
{
    public class DefaultSensor : IDistanceSensor
    {
        public double Angle { private set; get; }

        public DefaultSensor(IMaze maze, MobileRobot robot, double rotationAngle)
        {
            _maze = maze;
            _robot = robot;
            _rotationAngle = rotationAngle;

            _rayLength = 1.1 * maze.Diameter.Length;

            Reset();
        }

        private readonly IMaze _maze;
        private readonly MobileRobot _robot;
        private readonly double _rotationAngle;
        private readonly double _rayLength;
        private Line _ray;
        
        public void Rotate()
        {
            _ray = _ray.Rotate(_rotationAngle);

            Angle += _rotationAngle;
        }
        
        public DistanceSensorResult LookForward()
        {
            var haveGap = true;

            var result = new DistanceSensorResult();
            
            var distanceToObservedPoint = _rayLength;
            var currentIntersectionPoint = new Point();
            
            foreach (var wall in _maze.Walls)
            {
                if (!_ray.HaveIntersectionPoint(wall.Line, ref currentIntersectionPoint))
                    continue;

                if (haveGap)
                {
                    haveGap = false;

                    result = new DistanceSensorResult(currentIntersectionPoint, wall);

                    distanceToObservedPoint = _robot.Position.GetDistanceTo(currentIntersectionPoint);
                }

                if (_robot.Position.GetDistanceTo(currentIntersectionPoint) < distanceToObservedPoint)
                {
                    result = new DistanceSensorResult(currentIntersectionPoint, wall);

                    distanceToObservedPoint = _robot.Position.GetDistanceTo(currentIntersectionPoint);
                }
            }

            if (haveGap)
                throw new MazeHaveGapException();
            
            return result;
        }

        public void Reset()
        {
            _ray = new Line(_robot.Position, _robot.Position + new Point(_rayLength, 0));

            Angle = 0;
        }
    }
}
