using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigation.App.Repository;
using Navigation.App.Views;

namespace Navigation.App.Controllers
{
    public class RepositoryController : IController
    {
        private IMazeRepository _repository;
        private IRepositoryView _view;

        public RepositoryController(IRepositoryView view, IMazeRepository repository)
        {
            _repository = repository;
            _view = view;

            _view.LoadingMaze += _repository.Loading;
            _view.SavingMaze += _repository.Saving;
            _view.DeletingMaze += _repository.Deleting;

            _repository.CommandExecuted += _view.OnRepositoryCommandExecuted;
            _repository.CommandError += _view.OnRepositoryCommandError;
            _repository.AddedMaze += _view.OnRepositoryAddedMaze;
            _repository.RemovedMaze += _view.OnRepositoryRemovedMaze;
        }
    }
}
