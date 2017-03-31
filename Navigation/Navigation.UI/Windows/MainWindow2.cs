using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Navigation.App.Extensions;
using Navigation.Domain.Repository;
using Navigation.UI.Windows.Controls;

namespace Navigation.UI.Windows
{
    public class MainWindow2 : BaseWindow
    {
        public GameViewer GameViewer { get; private set; }
        public Canvas Canvas { get; private set; }
        public MazeRepository Repository { get; }

        public MainWindow2()
        {
            WindowState = FormWindowState.Maximized;

            Repository = new MazeRepository("mazes/");

            GameViewer = new GameViewer();

            TopMenuStrip.WithItems(
                new ToolStripMenuItem("Робот").TuneItem()
                    .WithDropDownItems(
                        new ToolStripButton("Запустить").TuneItem()
                            .WithOnClick((sender, args) => GameViewer.RunRobot()),
                        new ToolStripMenuItem("Настройки").TuneItem()
                            .WithDropDownItems(
                                new ToolStripMenuItem("Алгоритм").TuneItem())),
                new ToolStripMenuItem("Лабиринт").TuneItem()
                    .WithDropDownItems(
                        new ToolStripButton("Репозиторий лабиринтов").TuneItem()
                            .WithOnClick((sender, args) =>
                            {
                                /*var window = new BaseWindow()
                                {
                                    Height = 300
                                };

                                var table = new TableLayoutPanel().TuneControl();
                                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                                table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                                table.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
                                table.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
                                table.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));
                                table.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));

                                var mazeNames = Repository.GetMazeNames().ToList();

                                var listBox = new ListBox().TuneControl();
                                listBox.Items.AddRange(mazeNames.ToArray());

                                table.Controls.Add(listBox, 0, 0);
                                table.Controls.Add(new Button().TuneControl()
                                    .WithText("Загрузить")
                                    .WithOnClick((s, a) =>
                                    {
                                        if (listBox.SelectedIndex == -1)
                                            return;

                                        var mazeName = mazeNames[listBox.SelectedIndex];

                                        GameViewer.MazeViewer.StandartMaze = Repository.Loading(mazeName);

                                        Canvas.Invalidate();

                                        window.Close();
                                    }), 0, 1);
                                table.Controls.Add(new Button().TuneControl()
                                    .WithText("Удалить")
                                    .WithOnClick((s, a) =>
                                    {
                                        if (listBox.SelectedIndex == -1)
                                            return;

                                        var mazeName = mazeNames[listBox.SelectedIndex];

                                        mazeNames.RemoveAt(listBox.SelectedIndex);
                                        listBox.Items.RemoveAt(listBox.SelectedIndex);

                                        Repository.Deleting(mazeName);
                                    }), 0, 2);
                                table.Controls.Add(new Button().TuneControl()
                                    .WithText("Сохранить текущий")
                                    .WithOnClick((s, a) =>
                                    {
                                        string name = "";

                                        var windowSave = new BaseWindow()
                                        {
                                            Height = 300
                                        };

                                        var tableSave = new TableLayoutPanel().TuneControl();
                                        tableSave.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                                        tableSave.RowStyles.Add(new RowStyle(SizeType.Percent, 30));
                                        tableSave.RowStyles.Add(new RowStyle(SizeType.Percent, 70));
                                        tableSave.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));

                                        tableSave.Controls.Add(new Label().TuneControl()
                                            .WithText("Введите имя")
                                            .WithTextAlign(ContentAlignment.MiddleCenter), 0, 0);

                                        var mazeName = new TextBox().TuneControl()
                                            .WithBorderStyle(BorderStyle.FixedSingle)
                                            .WithTextAlign(HorizontalAlignment.Center);
                                        tableSave.Controls.Add(mazeName, 0, 1);

                                        tableSave.Controls.Add(new Button().TuneControl()
                                            .WithText("Сохранить")
                                            .WithOnClick((ss, aa) =>
                                            {
                                                name = mazeName.Text.Trim();

                                                if (name == "")
                                                    return;

                                                GameViewer.MazeViewer.SaveMaze(Repository, name);

                                                windowSave.Close();

                                                var resultWindow = new BaseWindow()
                                                {
                                                    Height = 300
                                                };
                                                resultWindow.MainPanel.Controls.Add(
                                                    new Label().TuneControl()
                                                        .WithText("Успешно сохранен!")
                                                        .WithTextAlign(ContentAlignment.MiddleCenter));
                                                resultWindow.ShowDialog();
                                            }), 0, 2);

                                        windowSave.MainPanel.Controls.Add(tableSave);

                                        windowSave.ShowDialog(this);

                                        mazeNames.Add(name);
                                        mazeNames.Sort();
                                        listBox.Items.Insert(mazeNames.IndexOf(name), name);
                                    }), 0, 4);

                                window.MainPanel.Controls.Add(table);

                                window.ShowDialog(this);*/
                            })),
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
                Canvas = new Canvas(this, GameViewer.MazeViewer.StandartMaze.Diameter)
                {
                    Dock = DockStyle.Fill
                };
                Canvas.Paint += (sender, args) => GameViewer.Draw(Canvas);
                MainPanel.Controls.Add(Canvas);
            };
        }
    }
}
