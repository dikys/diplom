using System;
using System.Collections.Generic;
using Navigation.Domain.Robot.Visions.Sensors;
using Navigation.Infrastructure;

namespace Navigation.Domain.Robot.Visions
{
    public class DefaultRobotVision : IRobotVision
    {
        private readonly double _minPassageSize;
        private readonly Lazy<IDistanceSensor> _distanceSensor;

        public DefaultRobotVision(Lazy<IDistanceSensor> distanceSensor, double minPassageSize)
        {
            _distanceSensor = distanceSensor;
            _minPassageSize = minPassageSize;
        }
        
        public VisionResult LookAround()
        {
            var observedСontour = new List<Line>();
            var finishPoint = new Point();

            return new VisionResult(LookAround(ref observedСontour, ref finishPoint), finishPoint, observedСontour, GetPassageInСontour(observedСontour));
        }
        
        private bool LookAround(ref List<Line> observedСontour, ref Point exitPoint)
        {
            _distanceSensor.Value.Reset();

            // Инициализация
            var distanceSensorResult = _distanceSensor.Value.LookForward();
            var sawFinish = distanceSensorResult.ObservedWall.IsFinish;

            var previousObservedPoint = distanceSensorResult.ObservedPoint;
            var previousObservedWall = distanceSensorResult.ObservedWall;

            var wallStart = distanceSensorResult.ObservedPoint;

            _distanceSensor.Value.Rotate();
            // Конец инициализации
            
            while (_distanceSensor.Value.Angle <= 2*Math.PI)
            {
                distanceSensorResult = _distanceSensor.Value.LookForward();

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

                _distanceSensor.Value.Rotate();
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
