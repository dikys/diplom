using System;
using System.Collections.Generic;
using System.Linq;
using Navigation.App.Common;
using Navigation.App.Common.Presenters;
using Navigation.App.Common.Views;
using Navigation.Domain.Game;

namespace Navigation.App.Presenters
{
    public class MainWindowPresenter : IMainWindowPresenter
    {
        private readonly IMainWindowView _mainWindowView;
        private readonly IGameModel _gameModel;
        private readonly List<IPresenter> _presenters;

        public bool IsShownView { get; private set; }

        public MainWindowPresenter(IMainWindowView mainWindowView, IGameModel gameModel, List<IPresenter> presenters)
        {
            _mainWindowView = mainWindowView;
            _gameModel = gameModel;
            _presenters = presenters;

            _mainWindowView.ShowViewOfPresenter += OnShowViewOfPresenter;

            IsShownView = false;
        }

        public void ShowView()
        {
            if (IsShownView)
                return;

            IsShownView = true;
            this._mainWindowView.Show();
        }

        public void CloseView()
        {
            this._mainWindowView.Close();
            IsShownView = false;
        }

        public void OnShowViewOfPresenter(Type presenterType) => _presenters.Single(presenterType.IsInstanceOfType).ShowView();
        public void OnSetMazeName(string name) => System.Console.WriteLine("MainWindowPresenter:OnSetMazeName - " + name);
    }
}
