using System;
using Navigation.Domain.Mazes;
using Navigation.Infrastructure;

namespace Navigation.Domain.Robot.Visions.Sensors
{
    public class DefaultSensor : IDistanceSensor
    {
        public double Angle { private set; get; }

        public DefaultSensor(IMaze maze, Lazy<MobileRobot> robot, double rotationAngle)
        {
            _maze = maze;
            _robot = robot;
            _rotationAngle = rotationAngle;

            _rayLength = 1.1 * maze.Diameter.Length;

            Reset();
        }

        private readonly IMaze _maze;
        private readonly Lazy<MobileRobot> _robot;
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

            DistanceSensorResult result = new DistanceSensorResult();
            
            var distanceToObservedPoint = 0.0;
            var currentIntersectionPoint = new Point();

            foreach (var wall in _maze.Walls)
            {
                if (!_ray.HaveIntersectionPoint(wall.Line, ref currentIntersectionPoint))
                    continue;

                if (haveGap || _robot.Value.Position.GetDistanceTo(currentIntersectionPoint) < distanceToObservedPoint)
                {
                    haveGap = false;

                    result = new DistanceSensorResult(currentIntersectionPoint, wall);

                    distanceToObservedPoint = _robot.Value.Position.GetDistanceTo(currentIntersectionPoint);
                }
            }

            if (haveGap)
                throw new InvalidOperationException("Border of maze has gap");
            
            return result;
        }

        public void Reset()
        {
            _ray = new Line(_robot.Value.Position, _robot.Value.Position + new Point(_rayLength, 0));

            Angle = 0;
        }
    }
}
