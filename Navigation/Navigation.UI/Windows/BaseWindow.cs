using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Navigation.App.Extensions;
using Navigation.UI.Extensions;
using Navigation.UI.Styles;

namespace Navigation.UI.Windows
{
    public class BaseWindow : Form
    {
        public MenuStrip TopMenuStrip;
        public Panel MainPanel;

        public BaseWindow()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterParent;
            
             var controlsTable = new TableLayoutPanel() { Dock = DockStyle.Fill }
                .WithColumnStyles(new ColumnStyle(SizeType.Percent, 100))
                .WithRowStyles(new RowStyle(SizeType.AutoSize, 30))
                .WithRowStyles(new RowStyle(SizeType.Percent, 100));
            Controls.Add(controlsTable);
            
            TopMenuStrip = new MenuStrip()
                .TuneControl()
                .WithRender(new BaseMenuStripRender(new BaseColorTable()))
                .OnMouseDown((s, e) =>
                {
                    base.Capture = false;
                    Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
                    WndProc(ref m);
                })
                .WithItems(
                    new ToolStripButton("Закрыть")
                        .TuneItem()
                        .WithAlignment(ToolStripItemAlignment.Right)
                        .WithOnClick((sender, args) => Close()));
            controlsTable.Controls.Add(TopMenuStrip, 0, 0);

            MainPanel = new Panel().TuneControl();
            MainPanel.BackColor = Color.FromArgb(225, 230, 250);
            MainPanel.BorderStyle = BorderStyle.FixedSingle;
            controlsTable.Controls.Add(MainPanel, 0, 1);
        }
    }
}
