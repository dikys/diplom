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

        public MainWindow()
        {
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;

            InitilizateWindow();

            Load += (s, e) =>
            {
                GameViewer = new GameViewer(Canvas);

                Canvas.Initilizate(GameViewer.MazeDiameter);
            };
        }

        public void InitilizateWindow()
        {
            var menuStripHeight = 30;

            var table = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(table);

            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            table.RowStyles.Add(new RowStyle(SizeType.Absolute, menuStripHeight));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var menu = SetDefaultSetting(new MenuStrip());
            menu.Renderer = new MenuStripRender();
            table.Controls.Add(menu, 0, 0);

            var robotRunButton = SetDefaultSettingForMenuItems(new ToolStripButton("Старт"));
            
            robotRunButton.Click += (sender, args) =>
            {
                GameViewer.RunRobot();
            };
            menu.Items.Add(robotRunButton);

            var item = SetDefaultSettingForMenuItems(new ToolStripMenuItem("Пункт 1"));
            menu.Items.Add(item);
            item.DropDownItems.AddRange(new ToolStripItem[]
            {
                new ToolStripButton("Привет"),
                new ToolStripLabel("Настройка ы")
            });

            item = SetDefaultSettingForMenuItems(new ToolStripMenuItem("Пункт 2"));
            menu.Items.Add(item);
            item.DropDownItems.AddRange(new ToolStripItem[]
            {
                new ToolStripLabel("Я лабел"),
                new ToolStripButton("Я кнопка")  
            });

            Canvas = new Canvas(this)
            {
                Dock = DockStyle.Fill
            };
            table.Controls.Add(Canvas, 0, 1);
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
