﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigation.Infrastructure;
using Point = Navigation.Infrastructure.Point;

namespace Navigation.App.Common.Views.Canvas
{
    public interface ICanvas
    {
        IFocus Focus { get; }

        event Action Paint;

        void ReDraw();

        void Draw(Point point, Color color, float size);
        void Draw(Line line, Color color);
        void Draw(Wall wall, Color color);
    }
}
