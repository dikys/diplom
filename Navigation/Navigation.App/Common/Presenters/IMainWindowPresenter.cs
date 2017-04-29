using System;

namespace Navigation.App.Common.Presenters
{
    public interface IMainWindowPresenter : IPresenter
    {
        void OnShowViewOfPresenter(Type presenterType);
        void OnSetMazeName(string name);
    }
}
