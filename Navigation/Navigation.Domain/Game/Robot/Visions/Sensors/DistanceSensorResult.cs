using Navigation.Infrastructure;

namespace Navigation.Domain.Game.Robot.Visions.Sensors
{
    public struct DistanceSensorResult
    {
        public DistanceSensorResult(Point observedPoint, Wall observedWall)
        {
            ObservedPoint = observedPoint;
            ObservedWall = observedWall;
        }
        
        public Point ObservedPoint;
        public Wall ObservedWall;
    }
}
