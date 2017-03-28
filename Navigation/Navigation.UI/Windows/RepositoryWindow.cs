using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Navigation.App.Extensions;
using Navigation.App.Views;
using Navigation.App.Windows;
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

            _listMazes = new ListBox().TuneControl();
            _listMazes.BackColor = Color.FromArgb(225, 230, 250);
            _listMazes.ForeColor = Color.FromArgb(55, 93, 129);
            _listMazes.DataSource = MazeNames;
            _listMazes.SelectedIndexChanged += (s, e) => SelectedName = _listMazes.SelectedItem.ToString();
            table.Controls.Add(_listMazes, 0, 0);

            table.Controls.Add(new Button()
                .TuneControl()
                .WithText("Загрузить [Не робит]")
                .WithOnClick((s, e) => LoadMaze?.Invoke()), 0, 1);
            table.Controls.Add(new Button()
                .TuneControl()
                .WithText("Сохранить [не робит]")
                .WithOnClick((s, e) => SaveMaze?.Invoke(new StandartMaze(), SelectedName)), 0, 2); // тут надо диалоговое окно использовать, которое стринг вернет))
            table.Controls.Add(new Button()
                .TuneControl()
                .WithText("Удалить")
                .WithOnClick((s, e) => DeleteMaze?.Invoke()), 0, 3);
            table.Controls.Add(new Button()
                .TuneControl()
                .WithText("Переименовать [не робит]")
                .WithOnClick((s, e) => ChangeMazeName?.Invoke(SelectedName)), 0, 4); // тут надо диалоговое окно использовать, которое стринг вернет))
        }

        public BindingList<string> MazeNames { get; }
        public string SelectedName { set; get; }

        public event Action LoadMaze;
        public event Action<IMaze, string> SaveMaze;
        public event Action DeleteMaze;
        public event Action<string> ChangeMazeName;

        public void SetMazeNames(List<string> names)
        {
            MazeNames.Clear();

            names.ForEach(MazeNames.Add);
            
            if (names.Any())
                _listMazes.SelectedIndex = names.IndexOf(SelectedName);
        }

        public void ShowError(string message)
        {
            var window = new BaseWindow();

            window.MainPanel.Controls.Add(new Label().TuneControl().WithText(message));

            window.ShowDialog();
        }
    }
}
