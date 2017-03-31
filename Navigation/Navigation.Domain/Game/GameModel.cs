using System;
using Navigation.Domain.Game.Mazes;
using Navigation.Domain.Game.Robot;

namespace Navigation.Domain.Game
{
    public class GameModel : IGameModel
    {
        private IMaze _maze;

        public GameModel(IMaze maze, IMobileRobot robot)
        {
            Maze = maze;
            Robot = robot;
        }

        public IMaze Maze
        {
            get { return _maze; }
            set { _maze = value; MazeChanged?.Invoke(); }
        }
        public IMobileRobot Robot { get; }

        public event Action MazeChanged;
    }
}
