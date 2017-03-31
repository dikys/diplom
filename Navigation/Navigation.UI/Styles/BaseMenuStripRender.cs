using System.Drawing;
using System.Windows.Forms;

namespace Navigation.UI.Styles
{
    public class BaseMenuStripRender : ToolStripProfessionalRenderer
    {
        public BaseMenuStripRender(ProfessionalColorTable table) : base(table) { }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(e.Item.Selected ? Color.FromArgb(24, 49, 82) : Color.FromArgb(55, 93, 129)),
                new Rectangle(Point.Empty, e.Item.Size));
        }
    }
}
