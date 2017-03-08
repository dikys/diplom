using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigation.Domain.Mazes;

namespace Navigation.App.Views
{
    public interface IRepositoryView
    {
        event Func<string, StandartMaze> LoadMaze;
        event Action<StandartMaze, string> SaveMaze;
        event Action<string> DeleteMaze;

        void OnRepositoryCommandExecuted(string commandName, string message);
        void OnRepositoryCommandError(string commandName, string message);
        void OnRepositoryAddedMaze(string name);
        void OnRepositoryRemovedMaze(string name);
    }
}
