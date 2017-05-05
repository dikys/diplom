namespace Navigation.App.Common.Presenters
{
    public interface IRepositoryPresenter : IPresenter
    {
        void LoadMaze(string name);

        void SaveMaze(string name);

        void DeleteMaze(string name);

        void ChangeMazeName(string nowName, string newName);
    }
}
