using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Navigation.Infrastructure;
using Newtonsoft.Json;

namespace Navigation.Domain.Maze
{
    public class Maze
    {
        public ImmutableList<Wall> Walls { get; set; }

        /// <summary>
        /// Диаметр лабиринта, у которой Start - верхний левый угол, а End - нижний правый угол
        /// </summary>
        private Line? _diameter;
        public Line Diameter
        {
            get
            {
                if (!_diameter.HasValue)
                {
                    //if (Walls.IsEmpty)
                        //throw new InvalidOperationException("Maze not have walls");

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

            return new Maze(); //Walls.AddRange(wall));
        }

        public Maze RemoveWall(params Wall[] wall)
        {
            if (wall == null)
                throw new ArgumentNullException("wall");

            return new Maze();//Walls.RemoveRange(wall));
        }

        #region Перегрузка Object методов
        public override string ToString()
        {
            return "Maze[" + String.Concat(Walls.Select((wall, index) => wall.ToString() + (index < Walls.Count ? "\n" : ""))) + "]";
        }
        #endregion
    }
}
