using System;
using System.Linq;
using Navigation.App.Views;
using Navigation.Domain.Game.Mazes;
using Navigation.Domain.Repository;

namespace Navigation.App.Presenters.Repository
{
    // переименовать в Presenter
    public class RepositoryPresenter : IRepositoryPresenter
    {
        private readonly IMazeRepository _repository;
        private readonly IRepositoryView _view;

        public RepositoryPresenter(IRepositoryView view, IMazeRepository repository)
        {
            _repository = repository;
            _view = view;

            _view.SetMazeNames(_repository.MazeNames.ToList());

            _view.LoadMaze += () => OnLoadMaze(_view.SelectedName);
            _view.SaveMaze += OnSaveMaze;
            _view.DeleteMaze += () => OnDeleteMaze(_view.SelectedName);
            _view.ChangeMazeName += (newName) => OnChangeMazeName(_view.SelectedName, newName);
        }

        public event Action<IMaze> LoadedMaze;

        public void OnLoadMaze(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (!_repository.HaveMaze(name))
            {
                _view.ShowError("Такого лабиринта не существует");

                return;
            }

            LoadedMaze?.Invoke(_repository.Load(name));
        }

        public void OnSaveMaze(IMaze maze, string name)
        {
            if (maze == null)
                throw new ArgumentNullException(nameof(maze));
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

            _repository.Save(maze, name);

            _view.SetMazeNames(_repository.MazeNames.ToList());
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

            _view.SelectedName = _view.MazeNames[Math.Max(_view.MazeNames.IndexOf(_view.SelectedName) - 1, 0)];
            _view.SetMazeNames(_repository.MazeNames.ToList()); // но это долго наверн
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

            _view.SelectedName = newName;
            _view.SetMazeNames(_repository.MazeNames.ToList()); // но это долго наверн
        }

        public void ShowView() => _view.Show();

        public void CloseView() => _view.Close();
    }
}
