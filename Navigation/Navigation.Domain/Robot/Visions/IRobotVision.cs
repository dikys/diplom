using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigation.Infrastructure;

namespace Navigation.Domain.Robot.Visions
{
    public interface IRobotVision
    {
        ImmutableList<ImmutableList<Line>> ViewedContours { get; }

        VisionResult LookAround();
    }
}
