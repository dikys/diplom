using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigation.Domain.Maze;
using Navigation.Infrastructure;

namespace Navigation.Domain.Robot
{
    public class Vision
    {
        /// <summary>
        /// Направление взгляда, вектор единичной длины
        /// </summary>
        public Point Direction { get; private set; }
        public double DirectionAngle { get; private set; }
        public Line DirectionTrace => new Line(_robot.Position, _robot.Position + Direction*_maze.Diameter.Length*2);
        public double RotationAngle { get; }

        private Maze.Maze _maze;
        private MobileRobot _robot;

        public Vision(Maze.Maze maze, MobileRobot robot)
        {
            _maze = maze;
            _robot = robot;

            RotationAngle = 0.005;

            ResetDirection();
        }
        
        /// <summary>
        /// Осмотреться. Вернет видимый контур.
        /// Если во время осмотра был найден выход, то прекратит восстанавливать видимый контур и вернет true.
        /// </summary>
        /// <returns></returns>
        public bool LookAround(ref List<Line> observedСontour, ref Point exitPoint)
        {
            ResetDirection();

            observedСontour = new List<Line>();
            exitPoint = new Point();
            
            Wall observedWall = new Wall(new Line(new Point(), new Point()));
            Point observedPoint = new Point();

            Point previousObservedPoint;
            Wall previousObservedWall;

            Point wallStart;

            // инициализация
            if (LookForward(ref observedPoint, ref observedWall))
                return true;
            
            previousObservedPoint = observedPoint.Clone();
            previousObservedWall = observedWall.Clone();

            wallStart = observedPoint.Clone();

            Rotate();
            // конец и.
            
            while (DirectionAngle <= 2*Math.PI)
            //while(observedContur.Count < 3)
            {
                if (LookForward(ref observedPoint, ref observedWall))
                {
                    return true;
                }

                if (!previousObservedWall.Equals(observedWall))
                {
                    observedСontour.Add(new Line(wallStart, previousObservedPoint));

                    wallStart = observedPoint.Clone();
                }

                previousObservedPoint = observedPoint.Clone();
                previousObservedWall = observedWall.Clone();

                Rotate();
            }

            observedСontour.Add(new Line(wallStart, previousObservedPoint));

            return false;
        }

        /// <summary>
        /// Виден ли финиш.
        /// </summary>
        private bool LookForward(ref Point observedPoint, ref Wall observedWall)
        {
            observedPoint = new Point();
            observedWall = null;

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

                    observedWall = wall.Clone();
                    observedPoint = currentIntersectionPoint.Clone();
                    distanceToObservedPoint = _robot.Position.GetDistanceTo(observedPoint);

                    continue;
                }

                if (_robot.Position.GetDistanceTo(currentIntersectionPoint) < distanceToObservedPoint)
                {
                    observedWall = wall.Clone();
                    observedPoint = currentIntersectionPoint.Clone();
                    distanceToObservedPoint = _robot.Position.GetDistanceTo(observedPoint);
                }
            }

            if (haveGap)
                throw new InvalidOperationException("Border of maze has gap");

            if (observedWall.IsFinish)
                return true;

            return false;
        }

        private void ResetDirection()
        {
            DirectionAngle = 0;
            Direction = new Point(1, 0);
        }

        private void Rotate()
        {
            DirectionAngle += RotationAngle;
            Direction = Direction.Rotate(RotationAngle);
        }
    }
}
