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
    class Canvas : PictureBox, ICanvas
    {
        public IFocus CanvasFocus { get; }
        public Graphics Graphics { get; private set; }

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
