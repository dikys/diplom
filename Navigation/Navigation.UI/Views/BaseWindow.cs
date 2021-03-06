﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Navigation.App.Extensions;
using Navigation.UI.Extensions;
using Navigation.UI.Styles;

namespace Navigation.UI.Views
{
    public class BaseWindow : Form
    {
        public MenuStrip TopMenuStrip;
        public Panel MainPanel;

        public BaseWindow()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterParent;

            var controlsTable = new TableLayoutPanel()
                .WithProperty("Dock", DockStyle.Fill)
                .WithColumnStyles(new ColumnStyle(SizeType.Percent, 100))
                .WithRowStyles(new RowStyle(SizeType.AutoSize, 30))
                .WithRowStyles(new RowStyle(SizeType.Percent, 100));
            Controls.Add(controlsTable);
            
            TopMenuStrip = new MenuStrip()
                .TuneControl()
                .WithProperty("Renderer", new BaseMenuStripRender(new BaseColorTable()))
                .WithMouseEventHandler("MouseDown", (s, e) =>
                {
                    base.Capture = false;
                    Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
                    WndProc(ref m);
                })
                .WithItems("Items",
                    new ToolStripButton("Закрыть")
                        .TuneItem()
                        .WithProperty("Alignment", ToolStripItemAlignment.Right)
                        .WithEventHandler("Click", (s, e) => Close()));

            controlsTable.Controls.Add(TopMenuStrip, 0, 0);
            
            MainPanel = new Panel()
                .TuneControl()
                .WithProperty("BackColor", Color.FromArgb(225, 230, 250))
                .WithProperty("BorderStyle", BorderStyle.FixedSingle);
            
            controlsTable.Controls.Add(MainPanel, 0, 1);
        }
    }
}
