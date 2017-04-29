using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Navigation.App.Extensions
{
    public static class ToolStripItemExtension
    {
        public static TItem TuneItem<TItem>(this TItem item)
            where TItem : ToolStripItem
        {
            item.Dock = DockStyle.Fill;
            item.ForeColor = Color.FromArgb(171, 200, 226);
            item.Width = 100;
            item.TextAlign = ContentAlignment.MiddleLeft;
            item.AutoToolTip = false;
            
            return item;
        }
    }
}
