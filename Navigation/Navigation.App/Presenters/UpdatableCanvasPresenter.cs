using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigation.App.Common.Presenters;
using Navigation.Infrastructure;
using Navigation.App.Common.Views.Canvas;

namespace Navigation.App.Presenters
{
    public class UpdatableCanvasPresenter : IUpdatableCanvasPresenter
    {
        private readonly ICanvas _canvas;

        public event Action CanvasPaint;
        
        UpdatableCanvasPresenter(ICanvas canvas)
        {
            _canvas = canvas;

            _canvas.Paint += () => CanvasPaint?.Invoke();
        }

        public void Draw(Infrastructure.Point point, Color color, float size = 2)
        {
            _canvas.Draw(point, color, size);
        }

        public void Draw(Line line, Color color)
        {
            _canvas.Draw(line, color);
        }

        public void Draw(Wall wall, Color color)
        {
            _canvas.Draw(wall, color);
        }
    }
}
