using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Navigation.Domain.Robot;
using Navigation.Domain.Robot.Visions;
using Navigation.Domain.Robot.Visions.Sensors;
using Navigation.Domain.Strategies.DFS;
using Navigation.Infrastructure;
using Ninject;

namespace Navigation.Domain.Test
{
    [TestClass]
    public class RobotWithDFSTest
    {
        [TestMethod]
        public void Should_b()
        {
            var container = MainFactory.CreateContainer<RobotWithDFS, DefaultRobotVision, DefaultSensor>(
                new Point(45, 45),
                MainFactory.GetDefaultMaze());

            var robot = (RobotWithDFS)container.Get<MobileRobot>();

            robot.Run();

            Assert.AreEqual(4, robot.RobotVision.Value.ViewedContours.Count);
            Assert.AreEqual(0, robot.WayToExit.Count);
        }
    }
}
