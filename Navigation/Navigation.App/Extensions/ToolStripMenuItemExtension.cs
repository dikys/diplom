using System.Windows.Forms;

namespace Navigation.App.Extensions
{
    public static class ToolStripMenuItemExtension
    {
        public static ToolStripMenuItem WithDropDownItems(this ToolStripMenuItem item, params ToolStripItem[] items)
        {
            item.DropDownItems.AddRange(items);

            return item;
        }
    }
}
