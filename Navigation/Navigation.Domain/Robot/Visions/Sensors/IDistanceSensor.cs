using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigation.Infrastructure;

namespace Navigation.Domain.Robot.Visions.Sensors
{
    public interface IDistanceSensor
    {
        double Angle { get; }

        DistanceSensorResult LookForward(Point fromPosition);
        void Rotate();
        void Reset();
    }
}
