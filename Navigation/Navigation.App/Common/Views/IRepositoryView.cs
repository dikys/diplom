using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Navigation.App.Common.Views
{
    public interface IRepositoryView : IView
    {
        BindingList<string> MazeNames { get; }
        string SelectedName { get; }

        event Action LoadingMaze;
        event Action SavingMaze;
        event Action DeletingMaze;
        event Action ChangingMazeName;

        void SetMazeNames(List<string> names);

        void SetSelectedName(string name);

        void ShowError(string message);
    }
}
