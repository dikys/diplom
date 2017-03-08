using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Navigation.Domain.Robot.Visions.Sensors;
using Navigation.Infrastructure;

namespace Navigation.Domain.Robot.Visions
{
    public class StandartVision : IRobotVision
    {
        private readonly double _minPassageSize;
        private readonly IDistanceSensor _distanceSensor;
        private readonly List<List<Line>> _viewedContours;
        
        public StandartVision(IDistanceSensor distanceSensor, double minPassageSize)
        {
            _distanceSensor = distanceSensor;
            _minPassageSize = minPassageSize;

            _viewedContours = new List<List<Line>>();
        }

        public ImmutableList<ImmutableList<Line>> ViewedContours => _viewedContours.Select(c => c.ToImmutableList()).ToImmutableList();
            
        public RobotVisionResult LookAround(Point fromPosition)
        {
            var observedСontour = new List<Line>();
            var finishPoint = new Point();
            var sawFinish = LookAround(fromPosition, ref observedСontour, ref finishPoint);
            var observedPassages = GetPassageInСontour(observedСontour);

            _viewedContours.Add(observedСontour);

            return new RobotVisionResult(sawFinish, finishPoint, observedСontour, observedPassages);
        }
        
        private bool LookAround(Point fromPosition, ref List<Line> observedСontour, ref Point finishPoint)
        {
            _distanceSensor.Reset();

            // Инициализация
            var distanceSensorResult = _distanceSensor.LookForward(fromPosition);
            var sawFinish = distanceSensorResult.ObservedWall.IsFinish;

            var previousObservedPoint = distanceSensorResult.ObservedPoint;
            var previousObservedWall = distanceSensorResult.ObservedWall;

            var wallStart = distanceSensorResult.ObservedPoint;

            _distanceSensor.Rotate();
            // Конец инициализации
            
            while (_distanceSensor.Angle <= 2*Math.PI)
            {
                distanceSensorResult = _distanceSensor.LookForward(fromPosition);

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

                _distanceSensor.Rotate();
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
