using System;
using System.Drawing;
using System.Windows.Forms;
using Navigation.App.Common.Views.Canvas;
using Navigation.App.Extensions;
using Navigation.Infrastructure;
using Point = Navigation.Infrastructure.Point;

namespace Navigation.UI.Views.Canvas
{
    public class Canvas : PictureBox, ICanvas
    {
        public Graphics Graphics { get; private set; }
        public bool IsFocused { get; private set; }
        
        public IFocus CanvasFocus { get; }
        IFocus ICanvas.Focus => CanvasFocus;

        event Action CanvasPaint;
        event Action ICanvas.Paint
        {
            add { CanvasPaint += value; }
            remove { CanvasPaint -= value; }
        }

        public Canvas(IFocus focus)
        {
            Dock = DockStyle.Fill;
            DoubleBuffered = true;
            ResizeRedraw = true;
            BackColor = Color.FromArgb(225, 230, 250);

            CanvasFocus = focus;
            CanvasFocus.Change += ReDraw;
            
            var previousMousePosition = new Point();
            MouseMove += (sender, args) =>
            {
                if (!IsFocused)
                    return;

                if (args.Button != MouseButtons.Left)
                {
                    previousMousePosition = new Point(args.X, Height - args.Y);

                    return;
                }

                var deltaPosition = new Point(args.X, Height - args.Y) - previousMousePosition;
                previousMousePosition = new Point(args.X, Height - args.Y);

                CanvasFocus.Move(deltaPosition);
            };

            Paint += (sender, args) =>
            {
                Graphics = args.Graphics;

                Graphics.MultiplyTransform(CanvasFocus.TransformMatrix);

                CanvasPaint?.Invoke();
            };

            MouseEnter += (sender, args) => IsFocused = true;
            MouseLeave += (sender, args) => IsFocused = false;
        }

        public void ReDraw()
        {
            Invalidate();
        }

        public void OnZoom(MouseEventArgs args)
        {
            if (!IsFocused)
                return;

            if (args.Delta > 0)
                CanvasFocus.ZoomIn();
            else
                CanvasFocus.ZoomOut();
        }

        public void Draw(Point point, Color color, float size = 2)
        {
            Graphics.FillEllipse(new SolidBrush(color), new RectangleF((point - size / 2).ToPointF(), new SizeF(size, size)));
        }

        public void Draw(Line line, Color color)
        {
            Graphics.DrawLine(new Pen(color), line.Start.ToPointF(), line.End.ToPointF());
        }

        public void Draw(Wall wall, Color color)
        {
            Draw(wall.Line, color);
        }
    }
}
