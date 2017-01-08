using System;
using System.Collections.Generic;
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
        protected TableLayoutPanel ControlsTable;
        protected MenuStrip TopMenuStrip;

        public BaseWindow()
        {
            FormBorderStyle = FormBorderStyle.None;

            ControlsTable = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill
            };
            ControlsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            ControlsTable.RowStyles.Add(new RowStyle(SizeType.AutoSize, 30));
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
        }
    }
}
