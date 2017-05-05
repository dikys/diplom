using System;
using System.Windows.Forms;
using Navigation.App.Common;
using Navigation.App.Common.Presenters;
using Navigation.App.Common.Views;
using Navigation.UI.Extensions;
using Canvas = Navigation.UI.Controls.Canvas;

namespace Navigation.UI.Windows
{
    public class MainWindow : BaseWindow, IMainWindowView
    {
        public MainWindow()
        {
            //WindowState = FormWindowState.Maximized;
            
            TopMenuStrip.WithItems("Items",
                new ToolStripButton("Запустить"),
                new ToolStripButton("Открыть Репозиторий")
                    .WithProperty("ToolTipText", "Репозиторий")
                    .WithEventHandler("Click", (s, e) => ShowingViewOfPresenter?.Invoke(typeof(IRepositoryPresenter))));

            FormClosed += (s, e) => ViewClosed?.Invoke();
        }

        public event Action<Type> ShowingViewOfPresenter;

        event Action ViewClosed;
        event Action IView.Closed
        {
            add { ViewClosed += value; }
            remove { ViewClosed -= value; }
        }

        public new void Show()
        {
            Application.Run(this);
        }
        
        void IView.Focus()
        {
            Focus();
        }
    }
}
