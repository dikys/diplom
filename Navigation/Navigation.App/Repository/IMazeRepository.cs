﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigation.Domain.Mazes;
using Newtonsoft.Json.Bson;

namespace Navigation.App.Repository
{
    public interface IMazeRepository
    {
        event Action<string, string> CommandExecuted;
        event Action<string, string> CommandError;
        event Action<string> AddedMaze;
        event Action<string> RemovedMaze;

        IEnumerable<string> MazeNames { get; }
        IEnumerable<StandartMaze> Mazes { get; }

        void Save(StandartMaze maze, string name);

        StandartMaze Load(string name);

        void Delete(string name);
    }
}
