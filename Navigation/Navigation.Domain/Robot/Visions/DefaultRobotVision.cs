using System;
using System.Collections.Generic;
using Navigation.Domain.Robot.Visions.Sensors;
using Navigation.Infrastructure;

namespace Navigation.Domain.Robot.Visions
{
    public class DefaultRobotVision : IRobotVision
    {
        private readonly double _minPassageSize;
        private readonly IDistanceSensor _distanceSensor;

        public DefaultRobotVision(IDistanceSensor distanceSensor, double minPassageSize)
        {
            _distanceSensor = distanceSensor;
            _minPassageSize = minPassageSize;
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
            _distanceSensor.Reset();

            // Инициализация
            var distanceSensorResult = _distanceSensor.LookForward();
            var sawFinish = distanceSensorResult.ObservedWall.IsFinish;

            var previousObservedPoint = distanceSensorResult.ObservedPoint;
            var previousObservedWall = distanceSensorResult.ObservedWall;

            var wallStart = distanceSensorResult.ObservedPoint;

            _distanceSensor.Rotate();
            // Конец инициализации
            
            while (_distanceSensor.Angle <= 2*Math.PI)
            {
                distanceSensorResult = _distanceSensor.LookForward();

                if (distanceSensorResult.ObservedWall.IsFinish)
                {
                    sawFinish = true;
                }

                if (!previousObservedWall.Equals(distanceSensorResult.ObservedWall))
                {
                    observedСontour.Add(new Line(wallStart, previousObservedPoint));

                    wallStart = distanceSensorResult.ObservedPoint;
                }

                previousObservedPoint = distanceSensorResult.ObservedPoint;
                previousObservedWall = distanceSensorResult.ObservedWall;

                _distanceSensor.Rotate();
            }

            observedСontour.Add(new Line(wallStart, previousObservedPoint));

            return sawFinish;
        }

        private List<Line> GetPassageInСontour(List<Line> contour)
        {
            var result = new List<Line>();

            for (var index = 0; index < contour.Count - 1; index++)
            {
                if (contour[index].End.GetDistanceTo(contour[index + 1].Start) > _minPassageSize)
                    result.Add(new Line(contour[index].End, contour[index + 1].Start));
            }

            if (contour[contour.Count - 1].End.GetDistanceTo(contour[0].Start) > _minPassageSize)
                result.Add(new Line(contour[contour.Count - 1].End, contour[0].Start));

            return result;
        }
    }
}
