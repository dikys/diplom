using System;
using System.Collections.Generic;
using Navigation.Domain.Maze;
using Navigation.Infrastructure;

namespace Navigation.Domain.Robot.Visions
{
    public class Vision
    {
        private Point _direction;
        private double _directionAngle;

        private Line DirectionTrace => new Line(_robot.Position, _robot.Position + _direction*_maze.Diameter.Length*2);
        private readonly double _rotationAngle;

        private Maze.Maze _maze;
        private MobileRobot _robot;

        public Vision(Maze.Maze maze, MobileRobot robot)
        {
            _maze = maze;
            _robot = robot;

            _rotationAngle = 0.005;

            ResetDirection();
        }

        public VisionResult LookAround()
        {
            var observedСontour = new List<Line>();
            var finishPoint = new Point();

            var sawFinish = LookAround(ref observedСontour, ref finishPoint);

            return new VisionResult(sawFinish, finishPoint, observedСontour, GetPassageInСontour(observedСontour));
        }
        
        private bool LookAround(ref List<Line> observedСontour, ref Point exitPoint)
        {
            ResetDirection();

            var sawFinish = false;
            
            var observedWall = new Wall(new Line(new Point(), new Point()));
            var observedPoint = new Point();

            // инициализация
            sawFinish = LookForward(ref observedPoint, ref observedWall);
            
            var previousObservedPoint = observedPoint;
            var previousObservedWall = observedWall;

            var wallStart = observedPoint;

            Rotate();
            // конец и.
            
            while (_directionAngle <= 2*Math.PI)
            {
                if (LookForward(ref observedPoint, ref observedWall))
                {
                    sawFinish = true;
                }

                if (!previousObservedWall.Equals(observedWall))
                {
                    observedСontour.Add(new Line(wallStart, previousObservedPoint));

                    wallStart = observedPoint;
                }

                previousObservedPoint = observedPoint;
                previousObservedWall = observedWall;

                Rotate();
            }

            observedСontour.Add(new Line(wallStart, previousObservedPoint));

            return sawFinish;
        }
        
        private bool LookForward(ref Point observedPoint, ref Wall observedWall)
        {
            var haveGap = true;
            
            var directionTrace = DirectionTrace;

            // вспомогательные переменные
            var distanceToObservedPoint = 0D;
            var currentIntersectionPoint = new Point();
            // конец в.п.

            foreach (var wall in _maze.Walls)
            {
                if (!directionTrace.HaveIntersectionPoint(wall.Line, ref currentIntersectionPoint))
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
        
        private List<Line> GetPassageInСontour(List<Line> contour)
        {
            var result = new List<Line>();

            for (var index = 0; index < contour.Count - 1; index++)
            {
                if (contour[index].End.GetDistanceTo(contour[index + 1].Start) > _robot.Size)
                    result.Add(new Line(contour[index].End, contour[index + 1].Start));
            }

            if (contour[contour.Count - 1].End.GetDistanceTo(contour[0].Start) > _robot.Size)
                result.Add(new Line(contour[contour.Count - 1].End, contour[0].Start));

            return result;
        }

        private void ResetDirection()
        {
            _directionAngle = 0;
            _direction = new Point(1, 0);
        }

        private void Rotate()
        {
            _directionAngle += _rotationAngle;
            _direction = _direction.Rotate(_rotationAngle);
        }
    }
}
