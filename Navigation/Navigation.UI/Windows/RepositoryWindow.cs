using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Navigation.App.Extensions;
using Navigation.App.Repository;
using Navigation.Domain.Game.Mazes;
using Navigation.UI.Extensions;

namespace Navigation.UI.Windows
{
    public class RepositoryWindow : BaseWindow, IRepositoryView
    {
        private readonly ListBox _listMazes;

        public RepositoryWindow()
        {
            Width = 800;
            Height = 600;

            MazeNames = new BindingList<string>();

            var table = new TableLayoutPanel()
                .TuneControl()
                .WithColumnStyles(new ColumnStyle(SizeType.Percent, 100))
                .WithRowStyles(new RowStyle(SizeType.Percent, 100))
                .WithRowStyles(new RowStyle(SizeType.Absolute, 30))
                .WithRowStyles(new RowStyle(SizeType.Absolute, 30))
                .WithRowStyles(new RowStyle(SizeType.Absolute, 30))
                .WithRowStyles(new RowStyle(SizeType.Absolute, 30));
            MainPanel.Controls.Add(table);

            _listMazes = new ListBox()
                .TuneControl()
                .WithProperty("BackColor", Color.FromArgb(225, 230, 250))
                .WithProperty("ForeColor", Color.FromArgb(55, 93, 129))
                .WithProperty("DataSource", MazeNames);
                //.OnEvent("SelectedIndexChanged", () => SelectedName = _listMazes.SelectedItem.ToString());

            _listMazes.SelectedIndexChanged +=
                (s, e) =>
                {
                    if (_listMazes.SelectedIndex != -1)
                        SelectedName = _listMazes.SelectedItem.ToString();
                    else
                    {
                        _listMazes.SelectedIndex = 0;

                        SelectedName = MazeNames[0];
                    }
                };
            table.Controls.Add(_listMazes, 0, 0);

            table.Controls.Add(new Button()
                .TuneControl()
                .WithText("Загрузить [Не робит]")
                .WithOnClick((s, e) => LoadMaze?.Invoke()), 0, 1);
            table.Controls.Add(new Button()
                .TuneControl()
                .WithText("Сохранить")
                .WithOnClick((s, e) => SaveMaze?.Invoke()), 0, 2); // тут надо диалоговое окно использовать, которое стринг вернет))
            table.Controls.Add(new Button()
                .TuneControl()
                .WithText("Удалить")
                .WithOnClick((s, e) => DeleteMaze?.Invoke()), 0, 3);
            table.Controls.Add(new Button()
                .TuneControl()
                .WithText("Переименовать")
                .WithOnClick((s, e) => ChangeMazeName?.Invoke()), 0, 4); // тут надо диалоговое окно использовать, которое стринг вернет))
        }

        public BindingList<string> MazeNames { get; }
        public string SelectedName { set; get; }

        public event Action LoadMaze;
        public event Action SaveMaze;
        public event Action DeleteMaze;
        public event Action ChangeMazeName;

        public void SetMazeNames(List<string> names)
        {
            MazeNames.Clear();

            names.ForEach(MazeNames.Add);

            if (SelectedName != null)
                SetSelected(SelectedName);
        }

        public void SetSelectedName(string name)
        {
            SetSelected(name);
        }

        public void ShowError(string message)
        {
            var window = new BaseWindow();

            window.MainPanel.Controls.Add(new Label().TuneControl().WithText(message));

            window.ShowDialog();
        }

        private void SetSelected(string name)
        {
            if (!MazeNames.Any())
                return;

            if (!MazeNames.Contains(name))
                return;

            _listMazes.SelectedIndex = MazeNames.IndexOf(name);
            SelectedName = name;
        }
    }
}
