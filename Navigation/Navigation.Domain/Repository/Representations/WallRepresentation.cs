using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigation.Domain.Maze;
using Navigation.Infrastructure;
using Newtonsoft.Json;

namespace Navigation.Domain.Repository.Representations
{
    internal class WallRepresentation
    {
        public LineRepresentation Line;
        public bool IsFinish;

        [JsonConstructor]
        public WallRepresentation(LineRepresentation line, bool isFinish)
        {
            Line = line;
            IsFinish = isFinish;
        }

        public WallRepresentation(Wall wall) : this(new LineRepresentation(wall.Line), wall.IsFinish) { }

        public static explicit operator Wall(WallRepresentation wallRepresentation)
        {
            return new Wall((Line)wallRepresentation.Line, wallRepresentation.IsFinish);
        }
    }
}
