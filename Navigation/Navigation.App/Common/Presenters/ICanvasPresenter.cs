using System;
using System.Drawing;
using Navigation.Infrastructure;
using Point = Navigation.Infrastructure.Point;

namespace Navigation.App.Common.Presenters
{
    public interface ICanvasPresenter
    {
        event Action CanvasPaint;

        void Draw(Point point, Color color, float size);
        void Draw(Line line, Color color);
        void Draw(Wall wall, Color color);
    }
}
