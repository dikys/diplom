using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Navigation.Domain.Exceptions;
using Navigation.Domain.Mazes;
using Navigation.Domain.Robot;
using Navigation.Domain.Robot.Visions;
using Navigation.Domain.Robot.Visions.Sensors;
using Navigation.Domain.Strategies.DFS;
using Navigation.Infrastructure;
using Ninject;

namespace Navigation.Domain.Test
{
    [TestClass]
    public class DefaultSensorTest
    {
        [TestMethod]
        public void Should_CorrectSensorResult_When_UseLookForward()
        {
            var container = MainFactory.CreateContainer<RobotWithDFS, DefaultRobotVision, DefaultSensor>(
                new Point(45, 45),
                new DefaultMaze(new Wall[]
                {
                    new Wall(new Line(70, -100, 70, 100)),
                    new Wall(new Line(60, -100, 60, 100)),
                    new Wall(new Line(75, -100, 75, 100))
                }),
                5,
                Math.PI / 4);

            var sensor = container.Get<IDistanceSensor>();

            sensor.Rotate();
            
            var sensorResult = sensor.LookForward();
            
            Assert.AreEqual(new Wall(new Line(60, -100, 60, 100)), sensorResult.ObservedWall);
            Assert.AreEqual(new Point(60, 60), sensorResult.ObservedPoint);
        }

        [TestMethod]
        public void Should_CorrectSensorResult_WhenDefaultMaze()
        {
            var container = MainFactory.CreateContainer<RobotWithDFS, DefaultRobotVision, DefaultSensor>(
                 new Point(45, 45),
                 MainFactory.GetDefaultMaze());

            var sensor = container.Get<IDistanceSensor>();

            var sensorResult = sensor.LookForward();

            Assert.AreEqual(new Point(95, 45), sensorResult.ObservedPoint);
            Assert.AreEqual(new Wall(new Line(75, 25, 100, 50)), sensorResult.ObservedWall);
        }

        [TestMethod]
        [ExpectedException(typeof (MazeHaveGapException))]
        public void Should_ThrowMazeHaveGapException_When_MizeHaveGap()
        {
            var container = MainFactory.CreateContainer<RobotWithDFS, DefaultRobotVision, DefaultSensor>(
                new Point(0, 0),
                new DefaultMaze(new Wall[]
                {
                    new Wall(new Line(10, 1, 10, -100))
                }),
                5,
                Math.PI / 4);

            var sensor = container.Get<IDistanceSensor>();

            sensor.Rotate();

            sensor.LookForward();
        }
    }
}
