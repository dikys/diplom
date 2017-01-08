using System;
using System.Drawing;
using System.Windows.Forms;
using Navigation.App.Extensions;
using Navigation.App.Windows.Controls;
using Point = System.Drawing.Point;

namespace Navigation.App.Windows
{
    public class MainWindow : BaseWindow
    {
        public GameViewer GameViewer { get; private set; }
        public Canvas Canvas { get; private set; }

        public MainWindow()
        {
            WindowState = FormWindowState.Maximized;

            GameViewer = new GameViewer();

            ControlsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            TopMenuStrip.WithItems(
                new ToolStripMenuItem("Робот")
                    .TuneItem()
                    .WithDropDownItems(
                        new ToolStripButton("Запустить")
                            .TuneItem()
                            .WithOnClick((sender, args) => GameViewer.RunRobot()),
                        new ToolStripButton("Остановить")
                            .TuneItem()
                            .WithOnClick((sender, args) => MessageBox.Show("Привет"))),
                new ToolStripMenuItem("Лабиринт")
                    .TuneItem()
                    .WithDropDownItems(
                        new ToolStripButton("Загрузить").TuneItem(),
                        new ToolStripButton("Настройка ы").TuneItem()),
                new ToolStripMenuItem("Редактор")
                    .TuneItem()
                    .WithDropDownItems(
                        new ToolStripButton("Открыть").TuneItem(),
                        new ToolStripButton("Отправить").TuneItem().WithToolTipText("Открыть редактор с текущим лабиринтом")));
            
            Load += (s, e) =>
            {
                Canvas = new Canvas(this, GameViewer.MazeDiameter)
                {
                    Dock = DockStyle.Fill
                };
                Canvas.Paint += (sender, args) => GameViewer.Draw(Canvas);
                ControlsTable.Controls.Add(Canvas, 0, 1);
            };
        }
    }
}
