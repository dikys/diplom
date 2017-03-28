using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Navigation.Domain.Game.Robot.Visions;
using Navigation.Domain.Game.Robot.Visions.Sensors;
using Navigation.Domain.Game.Strategies.DFS;
using Navigation.Infrastructure;
using Ninject;

namespace Navigation.Domain.Test
{
    [TestClass]
    public class StandartVisionTest
    {
        [TestMethod]
        public void Should_CorrectCountOfPassagesAndObservedContour_When_DefaultMaze()
        {
            var position = new Point(45, 45);

            var container = MainFactory.CreateContainer<RobotWithDFS, StandartVision, StandartSensor>(
                position,
                MainFactory.GetDefaultMaze());

            var vision = container.Get<IRobotVision>();

            var visionResult = vision.LookAround(position);

            Assert.AreEqual(2, visionResult.ObservedPassages.Count);
            Assert.AreEqual(9, visionResult.ObservedContour.Count);
        }

        [TestMethod]
        public void Should_TwoPassages_When_MinSize20()
        {
            var position = new Point(45, 45);

            var container = MainFactory.CreateContainer<RobotWithDFS, StandartVision, StandartSensor>(
                position,
                MainFactory.GetDefaultMaze(),
                20);

            var vision = container.Get<IRobotVision>();

            var visionResult = vision.LookAround(position);

            Assert.AreEqual(2, visionResult.ObservedPassages.Count);
        }

        [TestMethod]
        public void Should_ZeroPassages_When_MinSize30()
        {
            var position = new Point(45, 45);

            var container = MainFactory.CreateContainer<RobotWithDFS, StandartVision, StandartSensor>(
                position,
                MainFactory.GetDefaultMaze(),
                30);

            var vision = container.Get<IRobotVision>();

            var visionResult = vision.LookAround(position);

            Assert.AreEqual(0, visionResult.ObservedPassages.Count);
        }

        [TestMethod]
        public void Should_PassageCountedOnce()
        {
            var position = new Point(45, 45);

            var container = MainFactory.CreateContainer<RobotWithDFS, StandartVision, StandartSensor>(
                position,
                MainFactory.GetDefaultMaze());

            var vision = container.Get<IRobotVision>();

            var visionResult = vision.LookAround(position);

            Assert.AreEqual(2, visionResult.ObservedPassages.Count);

            visionResult = vision.LookAround(position);

            Assert.AreEqual(0, visionResult.ObservedPassages.Count);
        }

        [TestMethod]
        public void Should_FinishFound()
        {
            var position = new Point(45, 45);

            var container = MainFactory.CreateContainer<RobotWithDFS, StandartVision, StandartSensor>(
                position,
                MainFactory.GetDefaultMaze(0));

            var vision = container.Get<IRobotVision>();

            var visionResult = vision.LookAround(position);

            Assert.IsTrue(visionResult.SawFinish);
            Assert.AreEqual(0, (new Point(50, 25) - visionResult.FinishPoint).GetNorm(), 2);
        }
    }
}
