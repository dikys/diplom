using System.Windows.Forms;

namespace Navigation.App.Extensions
{
    public static class MenuStripExtension
    {
        public static MenuStrip WithItems(this MenuStrip menu, params ToolStripItem[] items)
        {
            menu.Items.AddRange(items);

            return menu;
        }

        public static MenuStrip WithRender(this MenuStrip menu, ToolStripRenderer render)
        {
            menu.Renderer = render;

            return menu;
        }
    }
}
