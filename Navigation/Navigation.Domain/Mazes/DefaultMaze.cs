using System;
using System.Collections.Immutable;
using System.Linq;
using Navigation.Infrastructure;

namespace Navigation.Domain.Mazes
{
    public class DefaultMaze : IMaze
    {
        private Line? _diameter;
        
        public DefaultMaze(ImmutableList<Wall> walls = null)
        {
            Walls = walls ?? ImmutableList<Wall>.Empty;
        }
        public DefaultMaze(params Wall[] walls) : this(walls.ToImmutableList())
        { }

        public ImmutableList<Wall> Walls { get; set; }
        /// <summary>
        /// Диаметр лабиринта, у которой Start - верхний левый угол, а End - нижний правый угол
        /// </summary>
        public Line Diameter
        {
            get
            {
                if (!_diameter.HasValue)
                {
                    _diameter = Walls.First().Line.Clone();

                    Walls.ForEach(wall =>
                    {
                        _diameter = new Line(
                            new Point(Math.Min(Math.Min(_diameter.Value.Start.X, wall.Line.Start.X), wall.Line.End.X),
                                Math.Max(Math.Max(_diameter.Value.Start.Y, wall.Line.Start.Y), wall.Line.End.Y)),
                            new Point(Math.Max(Math.Max(_diameter.Value.End.X, wall.Line.Start.X), wall.Line.End.X),
                                Math.Min(Math.Min(_diameter.Value.End.Y, wall.Line.Start.Y), wall.Line.End.Y)));
                    });
                }

                return _diameter.Value;
            }
        }

        public IMaze AddWalls(params Wall[] walls)
        {
            if (walls == null)
                throw new ArgumentNullException(nameof(walls));

            throw new NotImplementedException();
        }

        public IMaze RemoveWalls(params Wall[] walls)
        {
            if (walls == null)
                throw new ArgumentNullException(nameof(walls));

            throw new NotImplementedException();
        }

        #region Перегрузка Object методов
        public override string ToString()
        {
            return "DefaultMaze[" + String.Concat(Walls.Select((wall, index) => wall.ToString() + (index < Walls.Count ? "\n" : ""))) + "]";
        }
        #endregion
    }
}
