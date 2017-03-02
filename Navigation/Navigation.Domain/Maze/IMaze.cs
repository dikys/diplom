using Navigation.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigation.Domain.Maze
{
    public interface IMaze
    {
        ImmutableList<Wall> Walls { get; }
        Line Diameter { get; }

        IMaze AddWalls(params Wall[] walls);
        IMaze RemoveWalls(params Wall[] walls);
    }
}
