using System;
using Navigation.Domain.Game.Mazes;
using Navigation.Domain.Game.Robot;

namespace Navigation.Domain.Game
{
    public interface IGameModel
    {
        IMaze Maze { get; set; }
        IMobileRobot Robot { get; }

        event Action MazeChanged;
    }
}
