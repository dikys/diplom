using System.Collections.Immutable;
using Navigation.Infrastructure;

namespace Navigation.Domain.Game.Robot.Visions
{
    public interface IRobotVision
    {
        ImmutableList<ImmutableList<Line>> ViewedContours { get; }

        RobotVisionResult LookAround(Point fromPosition);
    }
}
