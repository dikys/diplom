using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigation.App.Presenters;

namespace Navigation.App.Views
{
    public interface IMainWindowView : IView
    {
        event Action<Type> ShowViewOfPresenter;
    }
}
