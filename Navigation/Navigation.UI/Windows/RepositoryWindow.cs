using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Navigation.App.Views;
using Navigation.App.Windows;
using Navigation.Domain.Mazes;

namespace Navigation.UI.Windows
{
    public class RepositoryWindow : BaseWindow, IRepositoryView
    {
        public event Func<string, StandartMaze> LoadingMaze;
        public event Action<StandartMaze, string> SavingMaze;
        public event Action<string> DeletingMaze;

        public void OnRepositoryCommandExecuted(string commandName, string message)
        {
            throw new NotImplementedException();
        }

        public void OnRepositoryCommandError(string commandName, string message)
        {
            throw new NotImplementedException();
        }

        public void OnRepositoryAddedMaze(string name)
        {
            throw new NotImplementedException();
        }

        public void OnRepositoryRemovedMaze(string name)
        {
            throw new NotImplementedException();
        }
    }
}
