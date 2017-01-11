using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Navigation.App.Extensions;
using Navigation.Domain.Maze;
using Navigation.Infrastructure;
using Point = Navigation.Infrastructure.Point;

namespace Navigation.App.Windows.Controls
{
    // Нужно еще сделать, чтобы фокус центрировался, если за граница лезет
    public class Canvas : PictureBox
    {
        #region Поля и свойства
        public Graphics Graphics { get; private set; }
        
        public bool IsFocused { get; private set; }

        public Focus Focus { get; private set; }

        private float _drawnPointSize = 2;
        #endregion
        
        public Canvas(Form form, Line maxFocus)
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            BackColor = Color.FromArgb(225, 230, 250);

            Focus = new Focus(maxFocus, this);
            Focus.Change += () => Invalidate();
            form.MouseWheel += (sender, args) =>
            {
                if (!IsFocused)
                    return;

                if (args.Delta > 0)
                    Focus.ZoomIn();
                else
                    Focus.ZoomOut();
            };
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
                
                Focus.Move(deltaPosition);
            };

            Paint += (sender, args) => InitilizateBeforePaint(args.Graphics);

            MouseEnter += (sender, args) => IsFocused = true;
            MouseLeave += (sender, args) => IsFocused = false;
        }
        
        #region Методы для рисование объектов
        public void Draw(Brush brush, Point point)
        {
            Graphics.FillEllipse(brush, new RectangleF((point - _drawnPointSize / 2).ToPointF(), new SizeF(_drawnPointSize, _drawnPointSize)));
        }
        
        public void Draw(Pen pen, Line line)
        {
            Graphics.DrawLine(pen, line.Start.ToPointF(), line.End.ToPointF());
        }

        public void Draw(Pen pen, Wall wall)
        {
            Draw(pen, wall.Line);
        }
        #endregion

        #region Реакции на события
        private void InitilizateBeforePaint(Graphics g)
        {
            Graphics = g;

            Graphics.MultiplyTransform(Focus.TransformMatrix);
        }
        #endregion
    }
}
