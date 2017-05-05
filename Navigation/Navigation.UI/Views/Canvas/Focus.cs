using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigation.App.Common.Views.Canvas;
using Navigation.App.Extensions;
using Navigation.Infrastructure;
using Point = Navigation.Infrastructure.Point;

namespace Navigation.UI.Views.Canvas
{
    class Focus : IFocus
    {
        private Line? _focusLine;
        public Line FocusLine
        {
            get { return _focusLine.Value; }
            private set
            {
                if (value.Equals(_focusLine))
                    return;

                var focusWidth = Math.Max(Math.Abs(value.Vector.X), Math.Abs(value.Vector.Y) * AspectRatio);
                var focusHeight = focusWidth / AspectRatio;

                if (Border.HasValue)
                {
                    _focusLine = new Line(Math.Max(value.Center.X - focusWidth / 2, Border.Value.Left),
                        Math.Max(value.Center.Y - focusHeight / 2, Border.Value.Top),
                        Math.Min(value.Center.X + focusWidth / 2, Border.Value.Right),
                        Math.Min(value.Center.Y + focusHeight / 2, Border.Value.Bottom));
                }
                else
                    _focusLine = new Line(value.Center.X - focusWidth / 2,
                        value.Center.Y - focusHeight / 2,
                        value.Center.X + focusWidth / 2,
                        value.Center.Y + focusHeight / 2);

                var scX = (float)(_canvasSize.Width / FocusLine.Vector.X);
                var scY = -(float)(_canvasSize.Height / FocusLine.Vector.Y);
                var trX = -(float)FocusLine.Start.X;
                var trY = -(float)FocusLine.Start.Y + _canvasSize.Height / scY;

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
            
            Recalculate(focusMaxLine);
        }

        public void Recalculate(Line focusMaxLine)
        {
            FocusLine = focusMaxLine;

            var focusMinHeight = Math.Max((float)FocusLine.Vector.Y * 10 / 100, 20);

            Border = new RectangleF(FocusLine.Start.ToPointF(), FocusLine.Vector.ToSizeF());
            MaxSize = new SizeF((float)Math.Abs(FocusLine.Vector.X), (float)Math.Abs(FocusLine.Vector.Y));
            MinSize = new SizeF((float)AspectRatio * focusMinHeight, focusMinHeight);

            Change?.Invoke();
        }

        public void ZoomIn()
        {
            if (FocusLine.Vector.X <= MinSize.Width || FocusLine.Vector.Y <= MinSize.Height)
                return;

            FocusLine = new Line(FocusLine.Start + ScalingSpeed * _focusCoefficient,
                FocusLine.End - ScalingSpeed * _focusCoefficient);
        }

        public void ZoomOut()
        {
            if (MaxSize.Width <= FocusLine.Vector.X || MaxSize.Height <= FocusLine.Vector.Y)
                return;

            FocusLine = new Line(FocusLine.Start - ScalingSpeed * _focusCoefficient,
                FocusLine.End + ScalingSpeed * _focusCoefficient);
        }

        public void Move(Point deltaPosition)
        {
            var newFocus = FocusLine - deltaPosition * MovingSpeed * _focusCoefficient;

            FocusLine = newFocus;
        }
    }
}
