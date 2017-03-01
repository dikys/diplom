using System;
using System.Collections.Generic;
using Navigation.Domain.Maze;
using Navigation.Infrastructure;

namespace Navigation.Domain.Robot.Visions
{
    public class Vision
    {
        public Vision(Sensor sensor, MobileRobot robot)
        {
            _sensor = sensor;
            _robot = robot;
        }

        private MobileRobot _robot;
        private Sensor _sensor;

        public VisionResult LookAround()
        {
            var observedСontour = new List<Line>();
            var finishPoint = new Point();

            var sawFinish = LookAround(ref observedСontour, ref finishPoint);

            return new VisionResult(sawFinish, finishPoint, observedСontour, GetPassageInСontour(observedСontour));
        }
        
        private bool LookAround(ref List<Line> observedСontour, ref Point exitPoint)
        {
            _sensor.Reset();

            var sawFinish = false;
            
            var observedWall = new Wall(new Line(new Point(), new Point()));
            var observedPoint = new Point();

            // Инициализация
            sawFinish = _sensor.LookForward(ref observedPoint, ref observedWall);
            
            var previousObservedPoint = observedPoint;
            var previousObservedWall = observedWall;

            var wallStart = observedPoint;

            _sensor.Rotate();
            // Конец инициализации
            
            while (_sensor.Angle <= 2*Math.PI)
            {
                if (_sensor.LookForward(ref observedPoint, ref observedWall))
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

                _sensor.Rotate();
            }

            observedСontour.Add(new Line(wallStart, previousObservedPoint));

            return sawFinish;
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
    }
}
