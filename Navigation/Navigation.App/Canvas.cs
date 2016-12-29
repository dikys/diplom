using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Navigation.Domain.Maze;
using Navigation.Infrastructure;
using Point = Navigation.Infrastructure.Point;

namespace Navigation.App
{
    // Нужно еще сделать, чтобы фокус центрировался, если за граница лезет
    public class Canvas : PictureBox
    {
        #region Поля и свойства
        public Graphics Graphics { get; private set; }
        
        public bool IsFocused { get; private set; }

        public double AspectRatio { get; private set; }
        
        public double FocusScalingSpeed { get; private set; }
        public double FocusMovingSpeed { get; private set; }

        public RectangleF FocusBorder {get; private set; }
        public SizeF FocusMaxSize { get; private set; }
        public SizeF FocusMinSize { get; private set; }

        public Matrix TransformCurrentMatrix { get; private set; }

        private double _focusCoefficient; 
        private Line? _currentFocus;
        public Line CurrentFocus
        {
            get { return _currentFocus.Value; }
            set
            {
                //if (value == null)
                //    throw new ArgumentNullException("CurrentFocus");

                if (value.Equals(_currentFocus))
                    return;

                if (_currentFocus.HasValue)
                    if (value.Start.X < FocusBorder.Left
                        || value.Start.Y < FocusBorder.Top
                        || FocusBorder.Right < value.End.X
                        || FocusBorder.Bottom < value.End.Y)
                        return;

                var focusWidth = Math.Max(Math.Abs(value.Vector.X), Math.Abs(value.Vector.Y)*AspectRatio);
                var focusHeight = focusWidth/AspectRatio;

                _currentFocus = new Line(value.Center.X - focusWidth/2,
                    value.Center.Y - focusHeight/2,
                    value.Center.X + focusWidth/2,
                    value.Center.Y + focusHeight/2);

                var scX = (float) (Width/CurrentFocus.Vector.X);
                var scY = -(float) (Height/CurrentFocus.Vector.Y);
                var trX = -(float) CurrentFocus.Start.X;
                var trY = -(float) CurrentFocus.Start.Y + Height/scY;

                TransformCurrentMatrix = new Matrix(scX, 0, 0, scY, scX*trX, scY*trY);

                _focusCoefficient = 1 / scX;

                Invalidate();
            }
        }

        private float _drawnPointSize = 2;
        #endregion
        
        public Canvas(Form form, Line focus)
        {
            DoubleBuffered = true;
            ResizeRedraw = true;

            FocusScalingSpeed = 30;
            FocusMovingSpeed = 1;
            BackColor = Color.FromArgb(225, 230, 250);

            /*LinePen = new Pen(Color.FromArgb(55, 93, 129));
            WallPen = LinePen;
            ExitWallPen = new Pen(Color.LawnGreen);
            PointBrush = new SolidBrush(Color.FromArgb(0, 47, 47));*/

            AspectRatio = (double)Width / Height;

            CurrentFocus = focus;

            var focusMinHeight = Math.Max((float)CurrentFocus.Vector.Y * 10 / 100, 20);

            FocusBorder = new RectangleF(CurrentFocus.Start.ToPointF(), CurrentFocus.Vector.ToSizeF());
            FocusMaxSize = new SizeF((float)Math.Abs(CurrentFocus.Vector.X), (float)Math.Abs(CurrentFocus.Vector.Y));
            FocusMinSize = new SizeF((float)AspectRatio * focusMinHeight, focusMinHeight);
            
            Paint += (sender, args) => InitilizateBeforePaint(args.Graphics);

            form.MouseWheel += (sender, args) =>
            {
                if (!IsFocused)
                    return;
                
                if (args.Delta > 0)
                    ZoomIn();
                else
                    ZoomOut();
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

                FocusMove(deltaPosition);
            };

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
        private void ZoomIn()
        {
            if (CurrentFocus.Vector.X <= FocusMinSize.Width || CurrentFocus.Vector.Y <= FocusMinSize.Height)
                return;

            CurrentFocus = new Line(CurrentFocus.Start + FocusScalingSpeed*_focusCoefficient,
                CurrentFocus.End - FocusScalingSpeed*_focusCoefficient);
        }

        private void ZoomOut()
        {
            if (FocusMaxSize.Width <= CurrentFocus.Vector.X || FocusMaxSize.Height <= CurrentFocus.Vector.Y)
                return;

            CurrentFocus = new Line(CurrentFocus.Start - FocusScalingSpeed*_focusCoefficient,
                CurrentFocus.End + FocusScalingSpeed*_focusCoefficient);
        }

        private void FocusMove(Point deltaPosition)
        {
            var newFocus = CurrentFocus - deltaPosition*FocusMovingSpeed*_focusCoefficient;
            
            CurrentFocus = newFocus;
        }

        private void InitilizateBeforePaint(Graphics g)
        {
            Graphics = g;

            Graphics.MultiplyTransform(TransformCurrentMatrix);
        }
        #endregion
    }
}
