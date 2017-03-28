using Navigation.Domain.Exceptions;
using Navigation.Domain.Game.Mazes;
using Navigation.Infrastructure;

namespace Navigation.Domain.Game.Robot.Visions.Sensors
{
    public class StandartSensor : IDistanceSensor
    {
        public double Angle { private set; get; }
        
        public StandartSensor(IMaze standartMaze, double rotationAngle)
        {
            _standartMaze = standartMaze;
            _rotationAngle = rotationAngle;

            _rayLength = 1.1 * standartMaze.Diameter.Length;

            Reset();
        }

        private readonly IMaze _standartMaze;
        private readonly double _rotationAngle;
        private readonly double _rayLength;
        private Line _ray;
        
        public void Rotate()
        {
            _ray = _ray.Rotate(_rotationAngle);

            Angle += _rotationAngle;
        }
        
        public DistanceSensorResult LookForward(Point fromPosition)
        {
            UpdateRay(fromPosition);

            var haveGap = true;

            var result = new DistanceSensorResult();
            var distanceToObservedPoint = _rayLength;
            var currentIntersectionPoint = new Point();

            _standartMaze.Walls.ForEach(wall =>
            {
                if (!_ray.CheckIntersectionPoint(wall.Line, ref currentIntersectionPoint))
                    return;

                if (haveGap || fromPosition.GetDistanceTo(currentIntersectionPoint) < distanceToObservedPoint)
                {
                    haveGap = false;

                    result = new DistanceSensorResult(currentIntersectionPoint, wall);

                    distanceToObservedPoint = fromPosition.GetDistanceTo(currentIntersectionPoint);
                }
            });
                
            if (haveGap)
                throw new MazeHaveGapException();
            
            return result;
        }
        
        public void Reset()
        {
            //_ray = new Line(_robot.Position, _robot.Position + new Point(_rayLength, 0));

            Angle = 0;
        }

        private void UpdateRay(Point start)
        {
            if (start == _ray.Start)
                return;

            _ray = new Line(start, start + new Point(_rayLength, 0)).Rotate(Angle);
        }
    }
}
