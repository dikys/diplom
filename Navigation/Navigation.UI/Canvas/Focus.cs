using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigation.App.Canvas;
using Navigation.App.Extensions;
using Navigation.Infrastructure;
using Point = Navigation.Infrastructure.Point;

namespace Navigation.UI.Canvas
{
    class Focus : IFocus
    {
        // Слева снизу начало
        private Line? _line;
        public Line Line
        {
            get { return _line.Value; }
            private set
            {
                if (value.Equals(_line))
                    return;

                var focusWidth = Math.Max(Math.Abs(value.Vector.X), Math.Abs(value.Vector.Y) * AspectRatio);
                var focusHeight = focusWidth / AspectRatio;

                if (Border.HasValue)
                {
                    _line = new Line(Math.Max(value.Center.X - focusWidth / 2, Border.Value.Left),
                        Math.Max(value.Center.Y - focusHeight / 2, Border.Value.Top),
                        Math.Min(value.Center.X + focusWidth / 2, Border.Value.Right),
                        Math.Min(value.Center.Y + focusHeight / 2, Border.Value.Bottom));
                }
                else
                    _line = new Line(value.Center.X - focusWidth / 2,
                        value.Center.Y - focusHeight / 2,
                        value.Center.X + focusWidth / 2,
                        value.Center.Y + focusHeight / 2);

                var scX = (float)(_canvasSize.Width / Line.Vector.X);
                var scY = -(float)(_canvasSize.Height / Line.Vector.Y);
                var trX = -(float)Line.Start.X;
                var trY = -(float)Line.Start.Y + _canvasSize.Height / scY;

                TransformMatrix = new Matrix(scX, 0, 0, scY, scX * trX, scY * trY);

                _focusCoefficient = 1 / scX;

                Change?.Invoke();
            }
        }
        public Matrix TransformMatrix { get; private set; }

        public double ScalingSpeed { get; }
        public double MovingSpeed { get; }

        public double AspectRatio { get; }

        public RectangleF? Border { get; private set; }
        public SizeF MaxSize { get; private set; }
        public SizeF MinSize { get; private set; }

        public event Action Change;

        private double _focusCoefficient;
        private Size _canvasSize;
        
        public Focus(Line focusMaxLine, Size canvasSize)
        {
            _canvasSize = canvasSize;

            ScalingSpeed = 30;
            MovingSpeed = 1;
            
            AspectRatio = _canvasSize.Width / _canvasSize.Height;
            
            RecalculateBorder(focusMaxLine);

            /*Line = focusMaxLine;

            var focusMinHeight = Math.Max((float)Line.Vector.Y * 10 / 100, 20);

            Border = new RectangleF(Line.Start.ToPointF(), Line.Vector.ToSizeF());
            MaxSize = new SizeF((float)Math.Abs(Line.Vector.X), (float)Math.Abs(Line.Vector.Y));
            MinSize = new SizeF((float)AspectRatio * focusMinHeight, focusMinHeight);*/
        }

        public void RecalculateBorder(Line focusMaxLine)
        {
            Line = focusMaxLine;

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
