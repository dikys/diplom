using System.Collections.Immutable;
using Navigation.Infrastructure;

namespace Navigation.Domain.Game.Mazes
{
    public interface IMaze
    {
        ImmutableList<Wall> Walls { get; }

        /// <summary>
        /// Диаметр лабиринта, у которой Start - верхний левый угол, а End - нижний правый угол
        /// </summary>
        Line Diameter { get; }

        IMaze AddWalls(params Wall[] walls);

        IMaze RemoveWalls(params Wall[] walls);
    }
}
