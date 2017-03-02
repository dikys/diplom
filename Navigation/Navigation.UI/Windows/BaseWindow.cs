using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Navigation.App.Extensions;
using Navigation.App.Windows.Controls;

namespace Navigation.App.Windows
{
    public class BaseWindow : Form
    {
        private TableLayoutPanel ControlsTable;

        public MenuStrip TopMenuStrip;
        public Panel MainPanel;

        public BaseWindow()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterParent;
            
             ControlsTable = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill
            };
            ControlsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            ControlsTable.RowStyles.Add(new RowStyle(SizeType.AutoSize, 30));
            ControlsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            Controls.Add(ControlsTable);
            
            TopMenuStrip = new MenuStrip()
                .TuneControl()
                .WithRender(new BaseMenuStripRender(new BaseColorTable()))
                .WithItems(
                    new ToolStripButton("Закрыть")
                        .TuneItem()
                        .WithAlignment(ToolStripItemAlignment.Right)
                        .WithOnClick((sender, args) => Close()));
            ControlsTable.Controls.Add(TopMenuStrip, 0, 0);

            MainPanel = new Panel().TuneControl();
            MainPanel.BackColor = Color.FromArgb(225, 230, 250);
            MainPanel.BorderStyle = BorderStyle.FixedSingle;

            ControlsTable.Controls.Add(MainPanel, 0, 1);
        }
    }
}
