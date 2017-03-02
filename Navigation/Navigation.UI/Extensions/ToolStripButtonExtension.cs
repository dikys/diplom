using System;
using System.Windows.Forms;

namespace Navigation.App.Extensions
{
    public static class ToolStripButtonExtension
    {
        public static ToolStripButton WithOnClick(this ToolStripButton button, EventHandler callback)
        {
            button.Click += callback;

            return button;
        }

        public static ToolStripButton WithToolTipText(this ToolStripButton button, string toolTipText)
        {
            button.ToolTipText = toolTipText;

            return button;
        }

        public static ToolStripButton WithAlignment(this ToolStripButton button, ToolStripItemAlignment alignment)
        {
            button.Alignment = alignment;

            return button;
        }
    }
}
