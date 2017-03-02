using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigation.Infrastructure;

namespace Navigation.Domain.Robot.Visions.Sensors
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
