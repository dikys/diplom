using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Navigation.Domain.Game.Robot;
using Navigation.Domain.Game.Robot.Visions;
using Navigation.Domain.Game.Robot.Visions.Sensors;
using Navigation.Domain.Game.Strategies.DFS;
using Navigation.Infrastructure;
using Ninject;

namespace Navigation.Domain.Test
{
    [TestClass]
    public class RobotWithDFSTest
    {
        [TestMethod]
        public void Should_FourViewedContours_When_DefaultMaze()
        {
            var container = MainFactory.CreateContainer<RobotWithDFS, StandartVision, StandartSensor>(
                new Point(45, 45),
                MainFactory.GetDefaultMaze());

            var robot = (RobotWithDFS)container.Get<IMobileRobot>();

            robot.Run();

            Assert.AreEqual(4, robot.RobotVision.ViewedContours.Count);
            Assert.AreEqual(0, robot.WayToExit.Count);
            Assert.IsFalse(robot.FinishFound);
        }

        [TestMethod]
        public void Should_TwoViewedContours_When_DefaultMazeHaveFinish()
        {
            var container = MainFactory.CreateContainer<RobotWithDFS, StandartVision, StandartSensor>(
                 new Point(45, 45),
                 MainFactory.GetDefaultMaze(3));

            var robot = (RobotWithDFS)container.Get<IMobileRobot>();

            robot.Run();

            Assert.AreEqual(2, robot.RobotVision.ViewedContours.Count);
            Assert.AreEqual(3, robot.WayToExit.Count);
            Assert.IsTrue(robot.FinishFound);
        }
    }
}
