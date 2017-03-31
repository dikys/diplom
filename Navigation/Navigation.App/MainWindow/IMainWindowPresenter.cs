using System;
using Navigation.App.Common;

namespace Navigation.App.MainWindow
{
    public interface IMainWindowPresenter : IPresenter
    {
        void OnShowViewOfPresenter(Type presenterType);
        void OnSetMazeName(string name);
    }
}
