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
                Renderer = new MenuStripRender(new TestColorTable())
            });
            WindowMainTable.Controls.Add(menu, 0, 0);

            var robotRunButton = SetDefaultSettingForMenuItems(new ToolStripButton("Старт"));
            robotRunButton.Click += (sender, args) => GameViewer.RunRobot();
            menu.Items.Add(robotRunButton);

            var item = SetDefaultSettingForMenuItems(new ToolStripMenuItem("Лабиринт")
            {
                DropDownItems =
                {
                    new ToolStripButton("Загрузить"),
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

            var closeButton = SetDefaultSettingForMenuItems(new ToolStripButton("Выход")
            {
                Alignment = ToolStripItemAlignment.Right
            });
            closeButton.Click += (sender, args) => Close();
            menu.Items.Add(closeButton);

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
            
            control.Padding = new Padding(0);

            control.BackColor = Color.FromArgb(55, 93, 129);
            control.ForeColor = Color.FromArgb(171, 200, 226);

            return control;
        }
        private TItem SetDefaultSettingForMenuItems<TItem>(TItem item)
            where TItem : ToolStripItem
        {
            item.Dock = DockStyle.Fill;

            item.Width = 100;
            
            return item;
        }

        public class TestColorTable : ProfessionalColorTable
        {
            /*Color culoare = Color.FromArgb(20, 20, 20);
            Color culoare1 = Color.FromArgb(36, 36, 36);

            // когда на кнопку нажали
            public override Color ButtonCheckedHighlight => Color.Blue;

            public override Color ButtonSelectedBorder => Color.Black;
            public override Color ButtonSelectedGradientBegin => Color.Black;
            public override Color ButtonSelectedGradientEnd => Color.Black;
            public override Color ButtonSelectedGradientMiddle => Color.Black;
            public override Color ButtonSelectedHighlight => Color.Black;
            public override Color ButtonSelectedHighlightBorder => Color.Black;
            public override Color CheckSelectedBackground => Color.Black;
            public override Color ButtonPressedHighlightBorder => Color.Black;

            //public override Color MenuItemBorder => Color.Black;
            //public override Color MenuItemPressedGradientBegin => Color.Black;
            //public override Color MenuItemPressedGradientEnd => Color.Black;
            public override Color MenuItemPressedGradientMiddle => Color.Black;
            public override Color MenuItemSelected => Color.Black;
            //public override Color MenuItemSelectedGradientBegin => Color.Black;
            //public override Color MenuItemSelectedGradientEnd => Color.Black;

            public override Color SeparatorDark => Color.Black;
            public override Color SeparatorLight => Color.Black;

            public override Color ToolStripBorder => Color.Black;
            public override Color ToolStripDropDownBackground => Color.Black;
            public override Color ToolStripGradientBegin => Color.Black;
            public override Color ToolStripGradientMiddle => Color.Black;
            public override Color ToolStripPanelGradientBegin => Color.Black;
            public override Color ToolStripPanelGradientEnd => Color.Black;*/

            public override Color ButtonCheckedGradientBegin => Color.Black;
            public override Color ButtonCheckedGradientEnd => Color.Black;
            public override Color ButtonCheckedGradientMiddle => Color.Black;
            public override Color ButtonCheckedHighlightBorder => Color.Black;
            public override Color ButtonPressedBorder => Color.Black;
            public override Color ButtonPressedGradientBegin => Color.Black;
            public override Color ButtonPressedGradientEnd => Color.Black;
            public override Color ButtonPressedGradientMiddle => Color.Black;
            public override Color ButtonPressedHighlight => Color.Black;
            public override Color ButtonPressedHighlightBorder => Color.Black;
            public override Color ButtonSelectedBorder => Color.Black;
            public override Color ButtonSelectedGradientBegin => Color.Black;
            public override Color ButtonSelectedGradientEnd => Color.Black;
            public override Color ButtonSelectedGradientMiddle => Color.Black;
            public override Color ButtonSelectedHighlight => Color.Black;
            public override Color ButtonSelectedHighlightBorder => Color.Black;
            public override Color CheckBackground => Color.Black;
            public override Color CheckPressedBackground => Color.Black;
            public override Color CheckSelectedBackground => Color.Black;
            public override Color GripDark => Color.Black;
            public override Color GripLight => Color.Black;
            public override Color ImageMarginGradientBegin => Color.Black;
            public override Color ImageMarginGradientEnd => Color.Black;
            public override Color ImageMarginGradientMiddle => Color.Black;
            public override Color ImageMarginRevealedGradientBegin => Color.Black;
            public override Color ImageMarginRevealedGradientEnd => Color.Black;
            public override Color ImageMarginRevealedGradientMiddle => Color.Black;
            public override Color MenuBorder => Color.Black;
            public override Color MenuItemBorder => Color.Black;
            public override Color MenuItemPressedGradientBegin => Color.Black;
            public override Color MenuItemPressedGradientEnd => Color.Black;
            public override Color MenuItemPressedGradientMiddle => Color.Black;
            public override Color MenuItemSelected => Color.Black;
            public override Color MenuItemSelectedGradientBegin => Color.Black;
            public override Color MenuItemSelectedGradientEnd => Color.Black;
            public override Color MenuStripGradientBegin => Color.Black;
            public override Color MenuStripGradientEnd => Color.Black;
            public override Color OverflowButtonGradientBegin => Color.Black;
            public override Color OverflowButtonGradientEnd => Color.Black;
            public override Color OverflowButtonGradientMiddle => Color.Black;
            public override Color RaftingContainerGradientBegin => Color.Black;
            public override Color RaftingContainerGradientEnd => Color.Black;
            public override Color SeparatorDark => Color.Black;
            public override Color SeparatorLight => Color.Black;
            public override Color StatusStripGradientBegin => Color.Black;
            public override Color StatusStripGradientEnd => Color.Black;
            public override Color ToolStripBorder => Color.Black;
            public override Color ToolStripContentPanelGradientBegin => Color.Black;
            public override Color ToolStripContentPanelGradientEnd => Color.Black;
            public override Color ToolStripDropDownBackground => Color.Black;
            public override Color ToolStripGradientBegin => Color.Black;
            public override Color ToolStripGradientEnd => Color.Black;
            public override Color ToolStripGradientMiddle => Color.Black;
            public override Color ToolStripPanelGradientBegin => Color.Black;
            public override Color ToolStripPanelGradientEnd => Color.Black;
        }

        public class MenuStripRender : ToolStripProfessionalRenderer
        {
            public MenuStripRender(ProfessionalColorTable table) : base(table) { }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                e.Graphics.FillRectangle(new SolidBrush(e.Item.Selected ? Color.FromArgb(24, 49, 82) : Color.FromArgb(55, 93, 129)),
                    new Rectangle(Point.Empty, e.Item.Size));
            }
        }
    }
}
