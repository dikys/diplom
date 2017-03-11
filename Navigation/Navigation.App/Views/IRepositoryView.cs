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
        event Func<string, StandartMaze> LoadingMaze;
        event Action<StandartMaze, string> SavingMaze;
        event Action<string> DeletingMaze;
        
        void OnRepositoryCommandExecuted(string commandName, string message);
        void OnRepositoryCommandError(string commandName, string message);
        void OnRepositoryAddedMaze(string name);
        void OnRepositoryRemovedMaze(string name);
    }
}
