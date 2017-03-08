using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigation.Domain.Mazes;
using Navigation.Domain.Robot;

namespace Navigation.Domain
{
    public class GameModel : IGameModel
    {
        private StandartMaze _maze;
        private MobileRobot _robot;

        public GameModel(StandartMaze maze, MobileRobot robot)
        {
            _maze = maze;
            _robot = robot;
        }


    }
}
