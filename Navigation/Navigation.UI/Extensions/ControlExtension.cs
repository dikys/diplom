using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Navigation.App.Dialogs;

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

        public static TControl WithText<TControl>(this TControl control, string text)
            where TControl : Control
        {
            control.Text = text;

            return control;
        }

        public static TControl WithControls<TControl>(this TControl control, params Control[] controls)
            where TControl : Control
        {
            control.Controls.AddRange(controls);

            return control;
        }

        public static TControl WithProperty<TControl>(this TControl control, string propertyName, Object value)
            where TControl : Control
        {
            control.GetType().GetProperty(propertyName).SetValue(control, value);

            return control;
        }

        /*public static TControl OnEvent<TControl>(this TControl control, string eventName, Action action)
            where TControl : Control
        {
            var tDelegate = control.GetType().GetEvent(eventName);
            
            Action<Object, EventArgs> ev = (sender, e) => action();

            Delegate d = Delegate.CreateDelegate(tDelegate.EventHandlerType, ev.Method);

            MethodInfo addHandler = tDelegate.GetAddMethod();
            Object[] addHandlerArgs = { d };
            addHandler.Invoke(control, addHandlerArgs);

            //control.GetType().GetEvent(eventName).AddEventHandler(control, Delegate.CreateDelegate(tDelegate.EventHandlerType, ev.Method));

            return control;
        }*/

        public static TControl WithTextAlign<TControl>(this TControl control, ContentAlignment alignment)
            where TControl : Label
        {
            control.TextAlign = alignment;

            return control;
        }

        public static TControl WithOnClick<TControl>(this TControl control, EventHandler callback)
            where TControl : Button
        {
            control.Click += callback;

            return control;
        }

        public static TControl WithBorderStyle<TControl>(this TControl control, BorderStyle style)
            where TControl : TextBox
        {
            control.BorderStyle = style;

            return control;
        }

        public static TControl WithTextAlign<TControl>(this TControl control, HorizontalAlignment align)
            where TControl : TextBox
        {
            control.TextAlign = align;

            return control;
        }
    }
}
