using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Point = System.Drawing.Point;

namespace Navigation.App
{
    public class MainWindow : Form
    {
        public GameViewer GameViewer { get; private set; }
        public Canvas Canvas { get; private set; }

        public TableLayoutPanel WindowMainTable;

        public MainWindow()
        {
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;

            GameViewer = new GameViewer();

            WindowMainTable = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill
            };
            WindowMainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            WindowMainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            WindowMainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            Controls.Add(WindowMainTable);

            var menu = SetDefaultSetting(new MenuStrip()
            {
                Renderer = new MenuStripRender()
            });
            WindowMainTable.Controls.Add(menu, 0, 0);

            var robotRunButton = SetDefaultSettingForMenuItems(new ToolStripButton("Старт"));
            robotRunButton.Click += (sender, args) => GameViewer.RunRobot();
            menu.Items.Add(robotRunButton);

            var item = SetDefaultSettingForMenuItems(new ToolStripMenuItem("Пункт 1")
            {
                DropDownItems =
                {
                    new ToolStripButton("Привет"),
                    new ToolStripLabel("Настройка ы")
                }
            });
            menu.Items.Add(item);

            item = SetDefaultSettingForMenuItems(new ToolStripMenuItem("Пункт 2")
            {
                DropDownItems =
                {
                    new ToolStripLabel("Я лабел"),
                    new ToolStripButton("Я кнопка")
                }
            });
            menu.Items.Add(item);

            Load += (s, e) =>
            {
                Canvas = new Canvas(this, GameViewer.MazeDiameter)
                {
                    Dock = DockStyle.Fill
                };
                WindowMainTable.Controls.Add(Canvas, 0, 1);

                Canvas.Paint += (sender, args) => GameViewer.Draw(Canvas);
            };
        }
        
        private TControl SetDefaultSetting<TControl>(TControl control)
            where TControl : Control
        {
            control.Dock = DockStyle.Fill;

            control.BackColor = Color.FromArgb(55, 93, 129);
            control.ForeColor = Color.FromArgb(171, 200, 226);

            return control;
        }
        private TItem SetDefaultSettingForMenuItems<TItem>(TItem item)
            where TItem : ToolStripItem
        {
            item.Dock = DockStyle.Fill;

            item.Width = 100;

            item.BackColor = Color.FromArgb(55, 93, 129);
            item.ForeColor = Color.FromArgb(171, 200, 226);

            return item;
        }

        private class MenuStripRender : ToolStripProfessionalRenderer
        {
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
                Color c = e.Item.Selected ? Color.FromArgb(24, 49, 82) : Color.FromArgb(55, 93, 129);
                using (SolidBrush brush = new SolidBrush(c))
                    e.Graphics.FillRectangle(brush, rc);
            }
        }
    }
}
