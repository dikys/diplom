using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigation.App.Canvas;
using Navigation.App.Common.Presenters;
using Navigation.Domain.Game;
using Navigation.Infrastructure;
using Point = Navigation.Infrastructure.Point;

namespace Navigation.App.Presenters
{
    public class CanvasPresenter : ICanvasPresenter
    {
        public event Action CanvasPaint;

        private readonly ICanvas _canvas;
        private readonly IGameModel _gameModel;

        public CanvasPresenter(ICanvas canvas, IGameModel gameModel)
        {
            _canvas = canvas;
            _gameModel = gameModel;

            _canvas.WFocus.Change += () => _canvas.ReDraw();
            _canvas.CanvasPaint += () =>
            {
                _gameModel.Maze.Walls.ForEach(w => _canvas.Draw(w, Color.Blue));
                Draw(_gameModel.Robot.Position, Color.Green);

                CanvasPaint?.Invoke();
            };
            _gameModel.MazeChanged += () =>
            {
                _canvas.WFocus.Recalculate(
                    new Line(gameModel.Maze.Diameter.Start.X,
                        gameModel.Maze.Diameter.End.Y,
                        gameModel.Maze.Diameter.End.X,
                        gameModel.Maze.Diameter.Start.Y));
                _canvas.ReDraw();
            };
        }

        public void Draw(Point point, Color color, float size = 2)
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
