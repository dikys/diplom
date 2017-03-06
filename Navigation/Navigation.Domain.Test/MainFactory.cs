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
                //.OnActivation(s => s.Reset());
            container.Bind<IRobotVision>()
                .To<TVision>()
                .InSingletonScope()
                .WithConstructorArgument("minPassageSize", minPassageSize);
            container.Bind<MobileRobot>()
                .To<TRobot>()
                .InSingletonScope()
                .WithConstructorArgument("position", robotPosition);
            
            container.Bind<Lazy<IDistanceSensor>>()
                .ToMethod(c => new Lazy<IDistanceSensor>(() => (c.Kernel.Get<IDistanceSensor>())))
                .InSingletonScope();

            /*container.Bind<Lazy<IRobotVision>>()
                .ToMethod(c => new Lazy<IRobotVision>(() => (c.Kernel.Get<IRobotVision>())));*/

            /*container.Bind<Lazy<MobileRobot>>()
                .ToMethod(c => new Lazy<MobileRobot>(() => c.Kernel.Get<MobileRobot>()));
                //.InSingletonScope();*/

            return container;
        }

        public static IMaze GetDefaultMaze(params int[] wallFinishIndexes)
        {
            var result = new DefaultMaze(
                new Wall[]
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
                }.Select((wall, index) => new Wall(wall.Line, wallFinishIndexes.Contains(index)))
                    .ToArray());

            return result;
        }
    }
}
