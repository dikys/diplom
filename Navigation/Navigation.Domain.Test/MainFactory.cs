using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Navigation.Domain.Robot.Visions.Sensors;
using Navigation.Domain.Robot;
using Navigation.Domain.Strategies.DFS;
using Navigation.Domain.Robot.Visions;
using Navigation.Infrastructure;
using Navigation.Domain.Mazes;

namespace Navigation.Domain.Test
{
    public static class MainFactory
    {
        public static StandardKernel CreateContainer<TRobot, TVision, TSensor>(Point robotPosition, IMaze maze, double minPassageSize = 5, double rotationAngle = 0.017)
            where TRobot : MobileRobot
            where TVision : IRobotVision
            where TSensor : IDistanceSensor
        {
            var container = new StandardKernel();

            container.Bind<IMaze>().ToConstant(maze).InSingletonScope();
            container.Bind<IDistanceSensor>()
                .To<TSensor>()
                .InSingletonScope()
                .WithConstructorArgument("rotationAngle", rotationAngle);
            container.Bind<IRobotVision>()
                .To<TVision>()
                .InSingletonScope()
                .WithConstructorArgument("minPassageSize", minPassageSize);
            container.Bind<MobileRobot>()
                .To<TRobot>()
                .InSingletonScope()
                .WithConstructorArgument("position", robotPosition);
            
            return container;
        }

        public static IMaze GetDefaultMaze()
        {
            return new DefaultMaze(new Wall[]
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
            });
        }
    }
}
