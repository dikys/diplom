using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigation.Domain.Game.Mazes;
using Navigation.Infrastructure;
using Point = Navigation.Infrastructure.Point;

namespace Navigation.App.Common.Views.Canvas
{
    public interface IFocus
    {
        /// <summary>
        /// Линия, которая показывает текущий фокус. Начало в левом нижнем угле.
        /// </summary>
        Line FocusLine { get; }

        Matrix TransformMatrix { get; }

        double ScalingSpeed { get; }

        double MovingSpeed { get; }

        /// <summary>
        /// Соотношение сторон Ширина : Высота
        /// </summary>
        double AspectRatio { get; }

        RectangleF? Border { get; }

        SizeF MaxSize { get; }
        SizeF MinSize { get; }

        event Action Change;
        
        void ZoomIn();
        void ZoomOut();
        void Move(Point deltaPosition);

        /// <summary>
        /// Пересчитать фокус относительно нового максимального размера, который задается focusMaxLine
        /// </summary>
        /// <param name="focusMaxLine"></param>
        void Recalculate(Line focusMaxLine);
    }
}
