using System;
using System.Linq;
using Navigation.App.Dialogs;
using Navigation.App.Dialogs.Elements;
using Navigation.App.Dialogs.Factoryes;
using Navigation.Domain.Game;
using Navigation.Domain.Repository;

namespace Navigation.App.Repository.Presenters
{
    // переименовать в Presenter
    public class RepositoryPresenter : IRepositoryPresenter
    {
        private readonly IMazeRepository _repository;
        private readonly IRepositoryView _view;
        private readonly IGameModel _gameModel;
        private readonly IDialogFactory _dialogFactory;

        public RepositoryPresenter(IRepositoryView view, IMazeRepository repository, IGameModel gameModel, IDialogFactory dialogFactory)
        {
            _repository = repository;
            _view = view;
            _gameModel = gameModel;
            _dialogFactory = dialogFactory;

            _view.SetMazeNames(_repository.MazeNames.ToList());
            
            _view.LoadMaze += () => OnLoadMaze(_view.SelectedName);
            _view.SaveMaze += () =>
            {
                var dialog = _dialogFactory.CreateDialog(new[]
                {
                    new DialogElement("Имя", DialogTypes.Input)
                });

                if (dialog.OpenDialog() == ResultOfDialog.Yes)
                {
                    OnSaveMaze(dialog.Elements.First().Value);
                }
            };
            _view.DeleteMaze += () => OnDeleteMaze(_view.SelectedName);
            _view.ChangeMazeName += () =>
            {
                var dialog = _dialogFactory.CreateDialog(new []
                {
                    new DialogElement("Старое имя", DialogTypes.Text, -1, _view.SelectedName), 
                    new DialogElement("Новое имя", DialogTypes.Input)
                });

                if (dialog.OpenDialog() == ResultOfDialog.Yes)
                {
                    OnChangeMazeName(_view.SelectedName, dialog.Elements[1].Value);
                }
            };
        }

        public void OnLoadMaze(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (!_repository.HaveMaze(name))
            {
                _view.ShowError("Такого лабиринта не существует");

                return;
            }

            _gameModel.Maze = _repository.Load(name);
        }

        public void OnSaveMaze(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

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

        public void OnDeleteMaze (string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

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

        public void OnChangeMazeName(string nowName, string newName)
        {
            if (nowName == null)
                throw new ArgumentNullException(nameof(nowName));
            if (newName == null)
                throw new ArgumentNullException(nameof(newName));

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
            _view.Show();
            _view.SetSelectedName(_view.MazeNames[0]);
        }

        public void CloseView() => _view.Close();
    }
}
