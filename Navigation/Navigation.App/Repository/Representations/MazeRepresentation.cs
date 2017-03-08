﻿using System.Collections.Generic;
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

        public MazeRepresentation(StandartMaze standartMaze) : this(standartMaze.Walls.Select(wall => new WallRepresentation(wall)).ToList()) { }

        public static explicit operator StandartMaze(MazeRepresentation mazeRepresentation)
        {
            return new StandartMaze(mazeRepresentation.Walls.Cast<Wall>().ToArray());
        }
    }
}
