using System.Collections.Generic;
using Navigation.Infrastructure;

namespace Navigation.Domain.Robot.Visions
{
    public struct VisionResult
    {
        public VisionResult(bool sawFinish,
            Point finishPoint,
            IReadOnlyList<Line> observedContour,
            IReadOnlyList<Line> observedPassages)
        {
            SawFinish = sawFinish;
            FinishPoint = finishPoint;
            ObservedPassages = observedPassages;
            ObservedContour = observedContour;
        }

        public bool SawFinish;
        public Point FinishPoint;
        public IReadOnlyList<Line> ObservedPassages;
        public IReadOnlyList<Line> ObservedContour;
    }
}
