﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigation.App.Extensions;
using Navigation.Infrastructure;
using Point = Navigation.Infrastructure.Point;

namespace Navigation.App.Windows.Controls
{
    public class Focus
    {
        private Line? _line;
        public Line Line
        {
            get { return _line.Value; }
            private set
            {
                if (value.Equals(_line))
                    return;

                if (_line.HasValue)
                    if (value.Start.X < Border.Left
                        || value.Start.Y < Border.Top
                        || Border.Right < value.End.X
                        || Border.Bottom < value.End.Y)
                        return;

                var focusWidth = Math.Max(Math.Abs(value.Vector.X), Math.Abs(value.Vector.Y) * AspectRatio);
                var focusHeight = focusWidth / AspectRatio;

                _line = new Line(value.Center.X - focusWidth / 2,
                    value.Center.Y - focusHeight / 2,
                    value.Center.X + focusWidth / 2,
                    value.Center.Y + focusHeight / 2);

                var scX = (float)(_canvas.Width / Line.Vector.X);
                var scY = -(float)(_canvas.Height / Line.Vector.Y);
                var trX = -(float)Line.Start.X;
                var trY = -(float)Line.Start.Y + _canvas.Height / scY;

                TransformMatrix = new Matrix(scX, 0, 0, scY, scX * trX, scY * trY);

                _focusCoefficient = 1 / scX;

                Change?.Invoke();
            }
        }

        public double AspectRatio { get; }
        public RectangleF Border { get; }
        public SizeF MaxSize { get; }
        public SizeF MinSize { get; }
        public double ScalingSpeed { get; }
        public double MovingSpeed { get; }
        public Matrix TransformMatrix { get; private set; }

        public event Action Change;

        private double _focusCoefficient;
        private Canvas _canvas;

        public Focus(Line maxLine, Canvas canvas)
        {
            ScalingSpeed = 30;
            MovingSpeed = 1;

            _canvas = canvas;

            AspectRatio = _canvas.Width/_canvas.Height;

            Line = maxLine;

            var focusMinHeight = Math.Max((float)Line.Vector.Y * 10 / 100, 20);

            Border = new RectangleF(Line.Start.ToPointF(), Line.Vector.ToSizeF());
            MaxSize = new SizeF((float)Math.Abs(Line.Vector.X), (float)Math.Abs(Line.Vector.Y));
            MinSize = new SizeF((float)AspectRatio * focusMinHeight, focusMinHeight);
        }

        public void ZoomIn()
        {
            if (Line.Vector.X <= MinSize.Width || Line.Vector.Y <= MinSize.Height)
                return;

            Line = new Line(Line.Start + ScalingSpeed * _focusCoefficient,
                Line.End - ScalingSpeed * _focusCoefficient);
        }

        public void ZoomOut()
        {
            if (MaxSize.Width <= Line.Vector.X || MaxSize.Height <= Line.Vector.Y)
                return;

            Line = new Line(Line.Start - ScalingSpeed * _focusCoefficient,
                Line.End + ScalingSpeed * _focusCoefficient);
        }

        public void Move(Point deltaPosition)
        {
            var newFocus = Line - deltaPosition * MovingSpeed * _focusCoefficient;

            Line = newFocus;
        }
    }
}
