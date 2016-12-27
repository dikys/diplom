using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigation.App
{
    public static class PointExtension
    {
        public static PointF ToPointF(this Infrastructure.Point point)
        {
            return new PointF((float)point.X, (float)point.Y);
        }

        public static SizeF ToSizeF(this Infrastructure.Point point)
        {
            return new SizeF((float)point.X, (float)point.Y);
        }
    }
}
