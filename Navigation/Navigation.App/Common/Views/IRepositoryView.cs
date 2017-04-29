using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Navigation.App.Common.Views
{
    public interface IRepositoryView : IView
    {
        BindingList<string> MazeNames { get; }
        string SelectedName { get; }

        event Action LoadMaze;
        event Action SaveMaze;
        event Action DeleteMaze;
        event Action ChangeMazeName;

        void SetMazeNames(List<string> names);

        void SetSelectedName(string name);

        void ShowError(string message);
    }
}
