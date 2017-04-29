namespace Navigation.App.Common.Presenters
{
    public interface IRepositoryPresenter : IPresenter
    {
        void OnLoadMaze(string name);

        void OnSaveMaze(string name);

        void OnDeleteMaze(string name);

        void OnChangeMazeName(string nowName, string newName);
    }
}
