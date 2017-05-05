﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigation.Domain.Game.Mazes;
using Navigation.Infrastructure;
using Point = Navigation.Infrastructure.Point;

namespace Navigation.App.Canvas
{
    public interface IFocus
    {
        Line FocusLine { get; }
        Matrix TransformMatrix { get; }
        double ScalingSpeed { get; }
        double MovingSpeed { get; }
        double AspectRatio { get; }
        RectangleF? Border { get; }
        SizeF MaxSize { get; }
        SizeF MinSize { get; }

        event Action Change;
        
        void ZoomIn();
        void ZoomOut();
        void Move(Point deltaPosition);

        void Recalculate(Line focusMaxLine);
    }
}
