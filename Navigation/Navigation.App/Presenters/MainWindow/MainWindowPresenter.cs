using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Navigation.App.Presenters.Repository;
using Navigation.App.Views;

namespace Navigation.App.Presenters.MainWindow
{
    public class MainWindowPresenter : IMainWindowPresenter
    {
        private readonly IMainWindowView _mainWindowView;
        private readonly List<IPresenter> _presenters;

        public MainWindowPresenter(IMainWindowView mainWindowView, List<IPresenter> presenters)
        {
            _mainWindowView = mainWindowView;
            _presenters = presenters;

            _mainWindowView.ShowViewOfPresenter += OnShowViewOfPresenter;
        }
        
        public void ShowView() => this._mainWindowView.Show();

        public void CloseView() => this._mainWindowView.Close();
        public void OnShowViewOfPresenter(Type presenterType) => _presenters.Single(presenterType.IsInstanceOfType).ShowView();
    }
}
