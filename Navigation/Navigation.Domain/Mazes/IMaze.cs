using System.Collections.Immutable;
using Navigation.Infrastructure;

namespace Navigation.Domain.Mazes
{
    public interface IMaze
    {
        ImmutableList<Wall> Walls { get; }
        Line Diameter { get; }

        IMaze AddWalls(params Wall[] walls);
        IMaze RemoveWalls(params Wall[] walls);
    }
}
