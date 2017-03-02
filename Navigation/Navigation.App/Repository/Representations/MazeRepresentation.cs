using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigation.Domain.Maze;
using Newtonsoft.Json;

namespace Navigation.Domain.Repository.Representations
{
    internal class MazeRepresentation
    {
        public List<WallRepresentation> Walls;

        [JsonConstructor]
        public MazeRepresentation(List<WallRepresentation> walls)
        {
            Walls = walls;
        }

        public MazeRepresentation(IMaze maze) : this(maze.Walls.Select(wall => new WallRepresentation(wall)).ToList()) { }

        public static explicit operator Maze.Maze(MazeRepresentation mazeRepresentation)
        {
            return new Maze.Maze(mazeRepresentation.Walls.Select(wall => (Wall)wall).ToArray());
        }
    }
}
