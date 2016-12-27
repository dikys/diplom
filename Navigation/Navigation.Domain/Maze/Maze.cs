using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigation.Infrastructure;

namespace Navigation.Domain.Maze
{
    public class Maze
    {
        public ImmutableList<Wall> Walls { get; }
        
        private Line _diameter;
        /// <summary>
        /// Вернет линию, которая является диаметром лабиринта, у которой Start - верхний левый угол, а End - нижний правый угол
        /// </summary>
        public Line Diameter
        {
            get
            {
                if (_diameter == null)
                {
                    if (Walls.IsEmpty)
                        throw new InvalidOperationException("Maze not have walls");

                    _diameter = Walls.First().Line.Clone();

                    Walls.ForEach(wall =>
                    {
                        _diameter = new Line(
                            new Point(Math.Min(Math.Min(_diameter.Start.X, wall.Line.Start.X), wall.Line.End.X),
                                Math.Max(Math.Max(_diameter.Start.Y, wall.Line.Start.Y), wall.Line.End.Y)),
                            new Point(Math.Max(Math.Max(_diameter.End.X, wall.Line.Start.X), wall.Line.End.X),
                                Math.Min(Math.Min(_diameter.End.Y, wall.Line.Start.Y), wall.Line.End.Y)));
                    });
                }

                return _diameter;
            }
        }

        public Maze(ImmutableList<Wall> walls = null)
        {
            Walls = walls ?? ImmutableList<Wall>.Empty;
        }

        public Maze(params Wall[] walls) : this(walls.ToImmutableList())
        { }

        public Maze AddWall(params Wall[] wall)
        {
            if (wall == null)
                throw new ArgumentNullException("wall");

            return new Maze(Walls.AddRange(wall));
        }

        public Maze RemoveWall(params Wall[] wall)
        {
            if (wall == null)
                throw new ArgumentNullException("wall");

            return new Maze(Walls.RemoveRange(wall));
        }

        public override string ToString()
        {
            return "Maze[" + String.Concat(Walls.Select((wall, index) => wall.ToString() + (index < Walls.Count ? "\n" : ""))) + "]";
        }
    }
}
