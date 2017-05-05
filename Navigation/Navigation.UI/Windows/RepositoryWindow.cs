using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Navigation.App.Common.Views;
using Navigation.App.Extensions;
using Navigation.UI.Extensions;

namespace Navigation.UI.Windows
{
    public class RepositoryWindow : BaseWindow, IRepositoryView
    {
        private readonly ListBox _listMazes;

        public RepositoryWindow(Size size)
        {
            Width = size.Width;
            Height = size.Height;

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
                //.WithEventHandler("SelectedIndexChanged", () => SelectedName = _listMazes.SelectedItem.ToString());

            _listMazes.SelectedIndexChanged +=
                (s, e) =>
                {
                    if (_listMazes.SelectedIndex != -1)
                    {
                        SelectedName = _listMazes.SelectedItem.ToString();
                    }
                    else
                    {
                        _listMazes.SelectedIndex = 0;

                        SelectedName = MazeNames[0];
                    }
                };
            table.Controls.Add(_listMazes, 0, 0);

            table.Controls.Add(new Button()
                .TuneControl()
                .WithProperty("Text", "Загрузить [Вроде меняет модель)]")
                .WithEventHandler("Click", (s, e) => LoadMaze?.Invoke()), 0, 1);
            table.Controls.Add(new Button()
                .TuneControl()
                .WithProperty("Text", "Сохранить")
                .WithEventHandler("Click", (s, e) => SaveMaze?.Invoke()), 0, 2);
            table.Controls.Add(new Button()
                .TuneControl()
                .WithProperty("Text", "Удалить")
                .WithEventHandler("Click", (s, e) => DeleteMaze?.Invoke()), 0, 3);
            table.Controls.Add(new Button()
                .TuneControl()
                .WithProperty("Text", "Переименовать")
                .WithEventHandler("Click", (s, e) => ChangeMazeName?.Invoke()), 0, 4);

            FormClosed += (s, e) => ViewClosed?.Invoke();
        }

        public BindingList<string> MazeNames { get; }
        public string SelectedName { set; get; }

        public event Action LoadMaze;
        public event Action SaveMaze;
        public event Action DeleteMaze;
        public event Action ChangeMazeName;
        public event Action ViewClosed;

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

            window.MainPanel.Controls.Add(new Label().TuneControl().WithProperty("Text", message));

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
