using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Navigation.App.Canvas;
using Navigation.App.Extensions;
using Navigation.Infrastructure;
using Point = Navigation.Infrastructure.Point;

namespace Navigation.UI.Canvas
{
    public class Canvas : PictureBox, ICanvas
    {
        public Graphics Graphics { get; private set; }
        public bool IsFocused { get; private set; }

        private readonly IFocus _focus;

        public Canvas(IFocus focus)
        {
            Dock = DockStyle.Fill;
            DoubleBuffered = true;
            ResizeRedraw = true;
            BackColor = Color.FromArgb(225, 230, 250);

            _focus = focus;

            _focus.Change += () => Invalidate();

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

                _focus.Move(deltaPosition);
            };

            Paint += (sender, args) =>
            {
                Graphics = args.Graphics;

                Graphics.MultiplyTransform(_focus.TransformMatrix);
            };

            MouseEnter += (sender, args) => IsFocused = true;
            MouseLeave += (sender, args) => IsFocused = false;
        }

        public void OnZoom(MouseEventArgs args)
        {
            if (!IsFocused)
                return;

            if (args.Delta > 0)
                _focus.ZoomIn();
            else
                _focus.ZoomOut();
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
