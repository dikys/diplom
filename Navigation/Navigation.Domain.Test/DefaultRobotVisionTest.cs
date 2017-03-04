using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Navigation.Domain.Robot.Visions;
using Navigation.Domain.Robot.Visions.Sensors;
using Navigation.Domain.Strategies.DFS;
using Navigation.Infrastructure;
using Ninject;

namespace Navigation.Domain.Test
{
    [TestClass]
    public class DefaultRobotVisionTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var container = MainFactory.CreateContainer<RobotWithDFS, DefaultRobotVision, DefaultSensor>(
                new Point(45, 45),
                MainFactory.GetDefaultMaze());

            var vision = container.Get<IRobotVision>();

            var visionResult = vision.LookAround();

            Assert.AreEqual(2, visionResult.ObservedPassages.Count);
            Assert.AreEqual(9, visionResult.ObservedContour.Count);
        }
    }
}
