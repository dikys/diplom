using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var container = new StandardKernel();

            container.Bind<IMaze>()
                .ToConstant(new DefaultMaze(new Wall[]
                {
                    new Wall(new Line(10, -100, 10, 100))
                })).InSingletonScope();
            container.Bind<MobileRobot>()
                .To<RobotWithDFS>()
                .InSingletonScope();
            //.WithConstructorArgument("position", new Point(0, 0));
            container.Bind<IRobotVision>()
                .To<DefaultRobotVision>()
                .InSingletonScope();
                //.WithConstructorArgument("minPassageSize", 5);
            container.Bind<IDistanceSensor>()
                .To<DefaultSensor>()
                .InSingletonScope();
            //.WithConstructorArgument("rotationAngle", 0.01);

            var sensor = container.Get<IDistanceSensor>();
            /*var sensor = MainFactory.CreateContainer<RobotWithDFS, DefaultRobotVision, DefaultSensor>(new Point(0, 0),
                new DefaultMaze(new Wall[]
                {
                    new Wall(new Line(10, -100, 10, 100))
                })).Get<DefaultSensor>();*/
            
            var sensorResult = sensor.LookForward();
            
            Assert.AreEqual(new Wall(new Line(10, -100, 10, 100)), sensorResult.ObservedWall);
            Assert.AreEqual(new Point(10, 0), sensorResult.ObservedPoint);
        }
    }
}
