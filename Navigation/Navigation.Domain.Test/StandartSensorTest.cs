using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Navigation.Domain.Exceptions;
using Navigation.Domain.Game.Mazes;
using Navigation.Domain.Game.Robot.Visions;
using Navigation.Domain.Game.Robot.Visions.Sensors;
using Navigation.Domain.Game.Strategies.DFS;
using Navigation.Infrastructure;
using Ninject;

namespace Navigation.Domain.Test
{
    [TestClass]
    public class StandartSensorTest
    {
        [TestMethod]
        public void Should_CorrectSensorResult_When_UseLookForward()
        {
            var position = new Point(45, 45);

            var container = MainFactory.CreateContainer<RobotWithDFS, StandartVision, StandartSensor>(
                position,
                new StandartMaze(new Wall[]
                {
                    new Wall(new Line(70, -100, 70, 100)),
                    new Wall(new Line(60, -100, 60, 100)),
                    new Wall(new Line(75, -100, 75, 100))
                }),
                5,
                Math.PI / 4);

            var sensor = container.Get<IDistanceSensor>();

            sensor.Rotate();
            
            var sensorResult = sensor.LookForward(position);
            
            Assert.AreEqual(new Wall(new Line(60, -100, 60, 100)), sensorResult.ObservedWall);
            Assert.AreEqual(new Point(60, 60), sensorResult.ObservedPoint);
        }

        [TestMethod]
        public void Should_CorrectSensorResult_WhenDefaultMaze()
        {
            var position = new Point(45, 45);

            var container = MainFactory.CreateContainer<RobotWithDFS, StandartVision, StandartSensor>(
                 new Point(position),
                 MainFactory.GetDefaultMaze());

            var sensor = container.Get<IDistanceSensor>();

            var sensorResult = sensor.LookForward(position);

            Assert.AreEqual(new Point(95, 45), sensorResult.ObservedPoint);
            Assert.AreEqual(new Wall(new Line(75, 25, 100, 50)), sensorResult.ObservedWall);
        }

        [TestMethod]
        [ExpectedException(typeof (MazeHaveGapException))]
        public void Should_ThrowMazeHaveGapException_When_MizeHaveGap()
        {
            var position = new Point();

            var container = MainFactory.CreateContainer<RobotWithDFS, StandartVision, StandartSensor>(
                position,
                new StandartMaze(new Wall[]
                {
                    new Wall(new Line(10, 1, 10, -100))
                }),
                5,
                Math.PI / 4);

            var sensor = container.Get<IDistanceSensor>();

            sensor.Rotate();

            sensor.LookForward(position);
        }
    }
}
