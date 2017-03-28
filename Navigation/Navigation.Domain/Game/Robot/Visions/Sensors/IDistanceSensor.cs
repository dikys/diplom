using Navigation.Infrastructure;

namespace Navigation.Domain.Game.Robot.Visions.Sensors
{
    public interface IDistanceSensor
    {
        double Angle { get; }

        DistanceSensorResult LookForward(Point fromPosition);
        void Rotate();
        void Reset();
    }
}
