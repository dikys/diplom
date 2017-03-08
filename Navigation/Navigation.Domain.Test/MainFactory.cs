using System.Linq;
using Ninject;
using Navigation.Domain.Robot.Visions.Sensors;
using Navigation.Domain.Robot;
using Navigation.Domain.Robot.Visions;
using Navigation.Infrastructure;
using Navigation.Domain.Mazes;

namespace Navigation.Domain.Test
{
    public static class MainFactory
    {
        public static StandardKernel CreateContainer<TRobot, TVision, TSensor>(Point robotPosition, StandartMaze standartMaze, double minPassageSize = 5, double rotationAngle = 0.017)
            where TRobot : MobileRobot
            where TVision : IRobotVision
            where TSensor : IDistanceSensor
        {
            var container = new StandardKernel();

            container.Bind<StandartMaze>().ToConstant(standartMaze).InSingletonScope();
            container.Bind<IDistanceSensor>()
                .To<TSensor>()
                .WithConstructorArgument("rotationAngle", rotationAngle);
            container.Bind<IRobotVision>()
                .To<TVision>()
                .WithConstructorArgument("minPassageSize", minPassageSize);
            container.Bind<MobileRobot>()
                .To<TRobot>()
                .WithConstructorArgument("position", robotPosition);

            return container;
        }

        public static StandartMaze GetDefaultMaze(params int[] wallFinishIndexes)
        {
            var result = new StandartMaze(
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
