namespace Navigation.App.Common
{
    public interface IPresenter
    {
        bool IsShownView { get; }

        void ShowView();
        void CloseView();
    }
}
