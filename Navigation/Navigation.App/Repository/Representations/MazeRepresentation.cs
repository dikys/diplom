using System.Collections.Generic;
using System.Linq;
using Navigation.Domain.Mazes;
using Navigation.Infrastructure;
using Newtonsoft.Json;

namespace Navigation.App.Repository.Representations
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

        public static explicit operator DefaultMaze(MazeRepresentation mazeRepresentation)
        {
            return new DefaultMaze(mazeRepresentation.Walls.Select(wall => (Wall)wall).ToArray());
        }
    }
}
