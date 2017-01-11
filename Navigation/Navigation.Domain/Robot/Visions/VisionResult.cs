using System.Collections.Generic;
using Navigation.Infrastructure;

namespace Navigation.Domain.Robot.Visions
{
    public struct VisionResult
    {
        public bool SawFinish;
        public Point FinishPoint;
        public IReadOnlyList<Line> ObservedPassages;
        public IReadOnlyList<Line> ObservedContour;

        public VisionResult(bool sawFinish, Point finishPoint, IReadOnlyList<Line> observedContour, IReadOnlyList<Line> observedPassages)
        {
            SawFinish = sawFinish;
            FinishPoint = finishPoint;
            ObservedPassages = observedPassages;
            ObservedContour = observedContour;
        }
    }
}
