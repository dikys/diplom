using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigation.App.Presenters.MainWindow
{
    public interface IMainWindowPresenter : IPresenter
    {
        void OnShowViewOfPresenter(Type presenterType);
    }
}
