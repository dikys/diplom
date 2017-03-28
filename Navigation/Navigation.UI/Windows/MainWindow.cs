using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Navigation.App.Extensions;
using Navigation.App.Presenters;
using Navigation.App.Presenters.Repository;
using Navigation.App.Views;

namespace Navigation.UI.Windows
{
    public class MainWindow : BaseWindow, IMainWindowView
    {
        public MainWindow()
        {
            TopMenuStrip.WithItems(
                new ToolStripButton("Открыть Репозиторий")
                    .WithToolTipText("Репозиторий")
                    .WithOnClick((s, e) => ShowViewOfPresenter?.Invoke(typeof(IRepositoryPresenter))));
        }

        public event Action<Type> ShowViewOfPresenter;

        public new void Show()
        {
            Application.Run(this);
        }
    }
}
