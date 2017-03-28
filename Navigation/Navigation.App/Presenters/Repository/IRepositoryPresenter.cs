using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigation.App.Views;
using Navigation.Domain.Game.Mazes;
using Navigation.Domain.Repository;

namespace Navigation.App.Presenters.Repository
{
    public interface IRepositoryPresenter : IPresenter
    {
        event Action<IMaze> LoadedMaze;

        void OnLoadMaze(string name);

        void OnSaveMaze(IMaze maze, string name);

        void OnDeleteMaze(string name);

        void OnChangeMazeName(string nowName, string newName);
    }
}
