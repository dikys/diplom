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
        public void Should_CorrectCountOfPassagesAndObservedContour_When_DefaultMaze()
        {
            var container = MainFactory.CreateContainer<RobotWithDFS, DefaultRobotVision, DefaultSensor>(
                new Point(45, 45),
                MainFactory.GetDefaultMaze());

            var vision = container.Get<IRobotVision>();

            var visionResult = vision.LookAround();

            Assert.AreEqual(2, visionResult.ObservedPassages.Count);
            Assert.AreEqual(9, visionResult.ObservedContour.Count);
        }

        [TestMethod]
        public void Should_TwoPassages_When_MinSize20()
        {
            var container = MainFactory.CreateContainer<RobotWithDFS, DefaultRobotVision, DefaultSensor>(
                new Point(45, 45),
                MainFactory.GetDefaultMaze(),
                20);

            var vision = container.Get<IRobotVision>();

            var visionResult = vision.LookAround();

            Assert.AreEqual(2, visionResult.ObservedPassages.Count);
        }

        [TestMethod]
        public void Should_ZeroPassages_When_MinSize30()
        {
            var container = MainFactory.CreateContainer<RobotWithDFS, DefaultRobotVision, DefaultSensor>(
                new Point(45, 45),
                MainFactory.GetDefaultMaze(),
                30);

            var vision = container.Get<IRobotVision>();

            var visionResult = vision.LookAround();

            Assert.AreEqual(0, visionResult.ObservedPassages.Count);
        }

        [TestMethod]
        public void Should_PassageCountedOnce()
        {
            var container = MainFactory.CreateContainer<RobotWithDFS, DefaultRobotVision, DefaultSensor>(
                new Point(45, 45),
                MainFactory.GetDefaultMaze());

            var vision = container.Get<IRobotVision>();

            var visionResult = vision.LookAround();

            Assert.AreEqual(2, visionResult.ObservedPassages.Count);

            visionResult = vision.LookAround();

            Assert.AreEqual(0, visionResult.ObservedPassages.Count);
        }

        [TestMethod]
        public void Should_FinishFound()
        {
            var container = MainFactory.CreateContainer<RobotWithDFS, DefaultRobotVision, DefaultSensor>(
                new Point(45, 45),
                MainFactory.GetDefaultMaze(0));

            var vision = container.Get<IRobotVision>();

            var visionResult = vision.LookAround();

            Assert.IsTrue(visionResult.SawFinish);
            Assert.AreEqual(0, (new Point(50, 25) - visionResult.FinishPoint).GetNorm(), 2);
        }
    }
}
