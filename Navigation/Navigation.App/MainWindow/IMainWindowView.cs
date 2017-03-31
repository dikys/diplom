using System;
using Navigation.App.Common;

namespace Navigation.App.MainWindow
{
    public interface IMainWindowView : IView
    {
        event Action<Type> ShowViewOfPresenter;
    }
}
