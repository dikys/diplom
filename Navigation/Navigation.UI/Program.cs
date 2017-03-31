using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Windows.Forms;
using Navigation.App.Common;
using Navigation.App.Dialogs;
using Navigation.App.Dialogs.Elements;
using Navigation.App.Dialogs.Factoryes;
using Navigation.UI.Windows;
using Navigation.App.MainWindow;
using Navigation.App.MainWindow.Presenters;
using Navigation.App.Repository;
using Navigation.App.Repository.Presenters;
using Navigation.Domain.Game;
using Navigation.Domain.Game.Mazes;
using Navigation.Domain.Game.Robot;
using Navigation.Domain.Game.Robot.Visions;
using Navigation.Domain.Game.Robot.Visions.Sensors;
using Navigation.Domain.Game.Strategies.DFS;
using Navigation.Domain.Repository;
using Navigation.Infrastructure;
using Navigation.UI.Dialogs;
using Ninject;
using Ninject.Parameters;

/*
 *  Планы:
 *      Создать фабрику, создающая робота, фабрика в конструктор принимает все возможные реализации робота
 *      и фабрику можно попросить создать робота передав ей тип
 *      
 *      Как-нить убрать из Line и Point tollerance
 *      
 *      Возможность визуального просмотра алгоритма
 *          Наверное, нужно для каждой стратегии создавать StrategyViewer, который все будет выводить
 *          А можно с помощью делегатов сделать
 *      
 *      Написать тесты для
 *          StandartVision
 *          StandartMaze
 *          ...
 *          
 *      Написать редактор
 *      
 *      Написать новую стратегию
 *          Которая не мгновенно перемещается в нужную точку, а двигается с некоторой скоростью
 *          Может быть он будет останавливаться и осматриваться
 *          
 *          
 */

namespace Navigation.UI
{
    public class Program
    {
        static IPresenter CreateMainPresenter()
        {
            var container = new StandardKernel();

            // игровая модель
            container.Bind<IGameModel>()
                .To<GameModel>()
                .InSingletonScope();
            container.Bind<IMaze>()
                .To<StandartMaze>()
                .InSingletonScope()
                .WithConstructorArgument("walls",
                    new[]
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
            container.Bind<IMobileRobot>()
                .To<RobotWithDFS>()
                .InSingletonScope()
                .WithConstructorArgument("position", new Point());
            container.Bind<IRobotVision>()
                .To<StandartVision>()
                .InSingletonScope()
                .WithConstructorArgument("minPassageSize", 5.0);
            container.Bind<IDistanceSensor>()
                .To<StandartSensor>()
                .InSingletonScope()
                .WithConstructorArgument("rotationAngle", 0.01);

            // репозиторий
            container.Bind<IRepositoryPresenter>()
                .To<RepositoryPresenter>()
                .InSingletonScope()
                .WithConstructorArgument("viewCreator",
                    (Func<IRepositoryView>) (() => container.Get<IRepositoryView>()));
            container.Bind<IRepositoryView>()
                .To<RepositoryWindow>();
            container.Bind<IMazeRepository>()
                .To<MazeRepository>()
                .InSingletonScope()
                .WithConstructorArgument("path", "mazes/");
            container.Bind<IDialogFactory>()
                .To<DialogFactory>();
            container.Bind<IDialogWindow>()
                .To<FirstDialogWindow>()
                .WithConstructorArgument("elements", new DialogElement[] {});

            // главное окно
            container.Bind<IMainWindowPresenter>()
                .To<MainWindowPresenter>()
                .InSingletonScope()
                .WithConstructorArgument("presenters",
                    new List<IPresenter>
                    {
                        container.Get<IRepositoryPresenter>()
                    });
            container.Bind<IMainWindowView>()
                .To<MainWindow>()
                .InSingletonScope();

            return container.Get<IMainWindowPresenter>();
        }

        static void Main(string[] args)
        {
            var mainPresenter = CreateMainPresenter();
            
            mainPresenter.ShowView();
        }
    }
}
