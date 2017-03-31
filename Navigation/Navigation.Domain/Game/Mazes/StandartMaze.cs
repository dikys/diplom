using System;
using System.Collections.Immutable;
using System.Linq;
using Navigation.Infrastructure;
using Ninject;

namespace Navigation.Domain.Game.Mazes
{
    public class StandartMaze : IMaze
    {
        private Line? _diameter;

        public StandartMaze(ImmutableList<Wall> walls = null)
        {
            Walls = walls ?? ImmutableList<Wall>.Empty;
        }
        [Inject]
        public StandartMaze(params Wall[] walls) : this(walls.ToImmutableList())
        { }

        public ImmutableList<Wall> Walls { get; }

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
            return "StandartMaze[" + String.Concat(Walls.Select((wall, index) => wall.ToString() + (index < Walls.Count ? "\n" : ""))) + "]";
        }
        #endregion
    }
}
