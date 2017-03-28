using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigation.Domain.Game.Mazes;

namespace Navigation.App.Views
{
    public interface IRepositoryView : IView
    {
        BindingList<string> MazeNames { get; }
        string SelectedName { set; get; }

        event Action LoadMaze;
        event Action<IMaze, string> SaveMaze;
        event Action DeleteMaze;
        event Action<string> ChangeMazeName;

        void SetMazeNames(List<string> names);

        void ShowError(string message);
    }
}
