using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Navigation.Domain.Robot.Visions.Sensors;
using Navigation.Infrastructure;

namespace Navigation.Domain.Robot.Visions
{
    public class DefaultRobotVision : IRobotVision
    {
        private readonly double _minPassageSize;
        private readonly Lazy<IDistanceSensor> _distanceSensor;
        private readonly List<List<Line>> _viewedContours;

        public Guid id;
        
        public DefaultRobotVision(Lazy<IDistanceSensor> distanceSensor, double minPassageSize)
        {
            _distanceSensor = distanceSensor;
            _minPassageSize = minPassageSize;

            _viewedContours = new List<List<Line>>();

            id = Guid.NewGuid();
            Console.WriteLine("IRobotVision создан " + id);
        }

        public ImmutableList<ImmutableList<Line>> ViewedContours => _viewedContours.Select(c => c.ToImmutableList()).ToImmutableList();
            
        public VisionResult LookAround()
        {
            var observedСontour = new List<Line>();
            var finishPoint = new Point();
            var sawFinish = LookAround(ref observedСontour, ref finishPoint);
            var observedPassages = GetPassageInСontour(observedСontour);

            _viewedContours.Add(observedСontour);

            return new VisionResult(sawFinish, finishPoint, observedСontour, observedPassages);
        }
        
        private bool LookAround(ref List<Line> observedСontour, ref Point finishPoint)
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

                if (distanceSensorResult.ObservedWall.IsFinish && !sawFinish)
                {
                    sawFinish = true;

                    finishPoint = distanceSensorResult.ObservedPoint;
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

            var passage = new Line();

            for (var index = 0; index < contour.Count - 1; index++)
            {
                if (contour[index].End.GetDistanceTo(contour[index + 1].Start) > _minPassageSize
                    && TryCreateNewPassage(contour[index].End, contour[index + 1].Start, ref passage))
                    result.Add(passage);
            }

            if (contour[contour.Count - 1].End.GetDistanceTo(contour[0].Start) > _minPassageSize
                && TryCreateNewPassage(contour[contour.Count - 1].End, contour[0].Start, ref passage))
                result.Add(passage);

            return result;
        }

        private bool TryCreateNewPassage(Point start, Point end, ref Line newPassage)
        {
            if (_viewedContours.SelectMany(contour => contour)
                .All(line => !line.HavePoint(start) && !line.HavePoint(end)))
            {
                newPassage = new Line(start, end);

                return true;
            }

            return false;
        }
    }
}
