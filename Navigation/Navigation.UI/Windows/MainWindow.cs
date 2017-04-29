using System;
using System.Windows.Forms;
using Navigation.App.Common.Presenters;
using Navigation.App.Common.Views;
using Navigation.App.Extensions;
using Navigation.App.MainWindow;
using Navigation.App.Repository;
using Navigation.UI.Extensions;

namespace Navigation.UI.Windows
{
    public class MainWindow : BaseWindow, IMainWindowView
    {
        public MainWindow()
        {
            WindowState = FormWindowState.Maximized;
            
            TopMenuStrip.WithItems("Items",
                new ToolStripButton("Запустить"),
                new ToolStripButton("Открыть Репозиторий")
                    .WithProperty("ToolTipText", "Репозиторий")
                    .WithEventHandler("Click", (s, e) => ShowViewOfPresenter?.Invoke(typeof(IRepositoryPresenter))));

           // MainPanel.Controls.
        }

        public event Action<Type> ShowViewOfPresenter;

        public new void Show()
        {
            Application.Run(this);
        }
    }
}
