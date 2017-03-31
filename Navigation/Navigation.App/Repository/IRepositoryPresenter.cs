using Navigation.App.Common;

namespace Navigation.App.Repository
{
    public interface IRepositoryPresenter : IPresenter
    {
        void OnLoadMaze(string name);

        void OnSaveMaze(string name);

        void OnDeleteMaze(string name);

        void OnChangeMazeName(string nowName, string newName);
    }
}
