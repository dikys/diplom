using System;
using System.Linq;
using Navigation.App.Common.Presenters;
using Navigation.App.Common.Views;
using Navigation.App.Dialogs;
using Navigation.App.Dialogs.Elements;
using Navigation.Domain.Game;
using Navigation.Domain.Repository;

namespace Navigation.App.Presenters
{
    // переименовать в Presenter
    public class RepositoryPresenter : IRepositoryPresenter
    {
        private readonly IMazeRepository _repository;
        private readonly Func<IRepositoryView> _viewCreator;
        private readonly IGameModel _gameModel;
        private readonly Func<DialogElement[], IDialogWindow> _dialogCreator;

        private IRepositoryView _view;

        public bool IsShownView { get; private set; }

        public RepositoryPresenter(
            IMazeRepository repository,
            IGameModel gameModel,
            Func<DialogElement[], IDialogWindow> dialogCreator,
            Func<IRepositoryView> viewCreator)
        {
            _repository = repository;
            _viewCreator = viewCreator;
            _gameModel = gameModel;
            _dialogCreator = dialogCreator;

            IsShownView = false;
        }

        public void LoadMaze(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            if (!_repository.HaveMaze(name))
            {
                _view.ShowError("Такого лабиринта не существует");

                return;
            }

            _gameModel.Maze = _repository.Load(name);
        }

        public void SaveMaze(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            name = name.Trim();

            if (name == "")
            {
                _view.ShowError("Имя лабиринта не может быть пустым");

                return;
            }
            if (_repository.HaveMaze(name))
            {
                _view.ShowError("Такой лабиринт уже существует");

                return;
            }

            _repository.Save(_gameModel.Maze, name);
            
            _view.SetMazeNames(_repository.MazeNames.ToList());
            _view.SetSelectedName(name);
        }

        public void DeleteMaze (string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            if (!_repository.HaveMaze(name))
            {
                _view.ShowError("Такого лабиринта не существует");

                return;
            }

            _repository.Delete(name);

            var index = _view.MazeNames.IndexOf(_view.SelectedName);

            _view.SetMazeNames(_repository.MazeNames.ToList());
            _view.SetSelectedName(_view.MazeNames[Math.Max(index - 1, 0)]);
        }

        public void ChangeMazeName(string nowName, string newName)
        {
            if (nowName == null) throw new ArgumentNullException(nameof(nowName));
            if (newName == null) throw new ArgumentNullException(nameof(newName));

            if (!_repository.HaveMaze(nowName))
            {
                _view.ShowError("Такого лабиринта не существует");

                return;
            }

            _repository.ChangeName(nowName, newName);
            
            _view.SetMazeNames(_repository.MazeNames.ToList());
            _view.SetSelectedName(newName);
        }

        public void ShowView()
        {
            if (IsShownView)
                return;

            IsShownView = true;

            _view = _viewCreator.Invoke();

            _view.SetMazeNames(_repository.MazeNames.ToList());

            _view.LoadMaze += () => LoadMaze(_view.SelectedName);
            _view.SaveMaze += () =>
            {
                var dialog = _dialogCreator(new[] { new DialogElement("Имя", DialogElementTypes.Input) });

                if (dialog.OpenDialog() == ResultOfDialog.Yes)
                {
                    SaveMaze(dialog.Elements.First().Value);
                }
            };
            _view.DeleteMaze += () => DeleteMaze(_view.SelectedName);
            _view.ChangeMazeName += () =>
            {
                var dialog = _dialogCreator(new[] {
                    new DialogElement("Старое имя", DialogElementTypes.Text, _view.SelectedName),
                    new DialogElement("Новое имя", DialogElementTypes.Input) });

                if (dialog.OpenDialog() == ResultOfDialog.Yes)
                {
                    ChangeMazeName(_view.SelectedName, dialog.Elements[1].Value);
                }
            };
            _view.ViewClosed += () => IsShownView = false;

            _view.Show();
            _view.SetSelectedName(_view.MazeNames[0]);
        }

        public void CloseView()
        {
            _view.Close();
            IsShownView = false;
        }
    }
}
