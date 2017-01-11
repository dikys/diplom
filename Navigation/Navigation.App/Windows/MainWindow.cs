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

            TopMenuStrip.WithItems(
                new ToolStripMenuItem("Робот").TuneItem()
                    .WithDropDownItems(
                        new ToolStripButton("Запустить").TuneItem()
                            .WithOnClick((sender, args) => GameViewer.RunRobot()),
                        new ToolStripButton("Остановить").TuneItem()
                            .WithOnClick((sender, args) =>
                            {
                                var window = new BaseWindow();

                                window.MainPanel.Controls.Add(new Label {Text = "Привет"});

                                window.ShowDialog();
                            })),
                new ToolStripMenuItem("Лабиринт").TuneItem()
                    .WithDropDownItems(
                        new ToolStripButton("Загрузить").TuneItem()
                            .WithOnClick((sender, args) => GameViewer.Load("maze2.txt")),
                        new ToolStripButton("охранить").TuneItem()
                            .WithOnClick((sender, args) => GameViewer.Save("maze.txt"))),
                new ToolStripMenuItem("Редактор").TuneItem()
                    .WithDropDownItems(
                        new ToolStripButton("Открыть").TuneItem(),
                        new ToolStripButton("Отправить").TuneItem()
                            .WithToolTipText("Открыть редактор с текущим лабиринтом")),
                new ToolStripButton("Настройки").TuneItem()
                    .WithOnClick((sender, args) =>
                    {
                        var window = new BaseWindow();

                        window.ShowDialog(this);
                    }));
            
            Load += (s, e) =>
            {
                Canvas = new Canvas(this, GameViewer.MazeDiameter)
                {
                    Dock = DockStyle.Fill
                };
                Canvas.Paint += (sender, args) => GameViewer.Draw(Canvas);
                MainPanel.Controls.Add(Canvas);
            };
        }
    }
}
