using System.Collections.Generic;
using Navigation.Domain.Game.Mazes;

namespace Navigation.Domain.Repository
{
    public interface IMazeRepository
    {
        IReadOnlyList<string> MazeNames { get; }

        bool HaveMaze(string name);

        void Save(IMaze maze, string name);
        IMaze Load(string name);
        void Delete(string name);
        void ChangeName(string nowName, string newName);
    }
}
