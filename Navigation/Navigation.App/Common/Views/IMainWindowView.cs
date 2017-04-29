using System;

namespace Navigation.App.Common.Views
{
    public interface IMainWindowView : IView
    {
        event Action<Type> ShowViewOfPresenter;
    }
}
