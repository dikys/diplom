using System;
using System.Windows.Forms;
using Navigation.App.Extensions;
using Navigation.App.MainWindow;
using Navigation.App.Repository;
using Navigation.App.Repository.Presenters;

namespace Navigation.UI.Windows
{
    public class MainWindow : BaseWindow, IMainWindowView
    {
        public MainWindow()
        {
            TopMenuStrip.WithItems(
                new ToolStripButton("Запустить"),
                new ToolStripButton("Открыть Репозиторий")
                    .WithToolTipText("Репозиторий")
                    .WithOnClick((s, e) => ShowViewOfPresenter?.Invoke(typeof(IRepositoryPresenter))));

           // MainPanel.Controls.
        }

        public event Action<Type> ShowViewOfPresenter;

        public new void Show()
        {
            Application.Run(this);
        }
    }
}
