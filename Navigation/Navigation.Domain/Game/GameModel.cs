using Navigation.Domain.Game.Mazes;
using Navigation.Domain.Game.Robot;

namespace Navigation.Domain.Game
{
    public class GameModel : IGameModel
    {
        private IMaze _maze;
        private IMobileRobot _robot;

        public GameModel(IMaze maze, IMobileRobot robot)
        {
            _maze = maze;
            _robot = robot;
        }


    }
}
