using System.Drawing;
using System.Windows.Forms;

namespace Navigation.App.Extensions
{
    public static class ControlExtension
    {
        public static TControl TuneControl<TControl>(this TControl control)
            where TControl : Control
        {
            control.Dock = DockStyle.Fill;
            control.Padding = new Padding(0);
            control.Margin = new Padding(0);
            control.BackColor = Color.FromArgb(55, 93, 129);
            control.ForeColor = Color.FromArgb(171, 200, 226);

            return control;
        }
    }
}
