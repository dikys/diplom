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
        // надо фабрику создать для создания всех объектов!
        public TSensor CreateSensor<TSensor>(Point robotPosition)
            where TSensor : IDistanceSensor
        {
            var container = new StandardKernel();

            container.Bind<MobileRobot>()
                .To<RobotWithDFS>()
                .InSingletonScope()
                .WithConstructorArgument("position", robotPosition);
            container.Bind<IMaze>().ToConstant(new DefaultMaze(new Wall[]
            {
                new Wall(new Line(50, 25, 75, 25)),
                new Wall(new Line(75, 25, 100, 50)),
                new Wall(new Line(100, 50, 100, 75)),
                new Wall(new Line(100, 75, 75, 100)),
                new Wall(new Line(75, 100, 50, 100)),
                new Wall(new Line(50, 100, 25, 75)),
                new Wall(new Line(25, 75, 25, 50)),
                new Wall(new Line(25, 50, 50, 25)),

                new Wall(new Line(75, 50, 75, 75)),
                new Wall(new Line(75, 75, 50, 75)),
                new Wall(new Line(50, 75, 75, 50))
            })).InSingletonScope();
            container.Bind<IRobotVision>()
                .To<DefaultRobotVision>()
                .InSingletonScope()
                .WithConstructorArgument("minPassageSize", 5);
            container.Bind<IDistanceSensor>()
                .To<TSensor>()
                .InSingletonScope()
                .WithConstructorArgument("rotationAngle", 0.01);

            return (TSensor)container.Get<IDistanceSensor>();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
