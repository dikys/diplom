using System;
using System.Collections.Generic;
using System.Drawing;
using Navigation.App.Common.Views.Canvas;
using Navigation.App.Common;
using Navigation.App.Common.Presenters;
using Navigation.App.Common.Views;
using Navigation.App.Dialogs;
using Navigation.App.Dialogs.Elements;
using Navigation.UI.Windows;
using Navigation.App.Presenters;
using Navigation.Domain.Game;
using Navigation.Domain.Game.Mazes;
using Navigation.Domain.Game.Robot;
using Navigation.Domain.Game.Robot.Visions;
using Navigation.Domain.Game.Robot.Visions.Sensors;
using Navigation.Domain.Game.Strategies.DFS;
using Navigation.Domain.Repository;
using Navigation.Infrastructure;
using Ninject;
using Ninject.Parameters;
using Navigation.UI.Controls.Canvas;
using Point = Navigation.Infrastructure.Point;

/*
 *  Планы:
 *      Создать фабрику, создающая робота, фабрика в конструктор принимает все возможные реализации робота
 *      и фабрику можно попросить создать робота передав ей тип
 *      
 *      Возможность визуального просмотра алгоритма
 *          Наверное, нужно для каждой стратегии создавать StrategyViewer, который все будет выводить
 *          А можно с помощью делегатов сделать
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
                .WithConstructorArgument("position", new Point(25, 25));
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
                    (Func<IRepositoryView>)
                        (() => container.Get<IRepositoryView>()))
                .WithConstructorArgument("dialogCreator",
                    (Func<DialogElement[], IDialogWindow>)
                        (elements => container.Get<IDialogWindow>(
                            new ConstructorArgument("elements", elements),
                            new ConstructorArgument("size", new Size(300, 100)))));
            container.Bind<IRepositoryView>()
                .To<RepositoryWindow>()
                .WithConstructorArgument("size",
                    new Size(350, 600));
            container.Bind<IMazeRepository>()
                .To<MazeRepository>()
                .InSingletonScope()
                .WithConstructorArgument("path", "mazes/");
            
            // диалоговые окна
            container.Bind<IDialogWindow>()
                .To<DialogWindow>();

            // холст
            var gameModel = container.Get<IGameModel>();

            container.Bind<IFocus>()
                .To<Focus>()
                .WithConstructorArgument("focusMaxLine",
                    new Line(gameModel.Maze.Diameter.Start.X, gameModel.Maze.Diameter.End.Y, gameModel.Maze.Diameter.End.X, gameModel.Maze.Diameter.Start.Y));
            container.Bind<ICanvas>()
                .To<Canvas>();
            container.Bind<ICanvasPresenter>()
                .To<CanvasPresenter>();
            
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
                .InSingletonScope()
                .OnActivation(p =>
                {
                    p.Shown += (m, a) =>
                    {
                        var focus = container.Get<IFocus>(new ConstructorArgument("canvasSize", p.MainPanel.Size));
                        var canvas = (Canvas) container.Get<ICanvas>(new ConstructorArgument("focus", focus));

                        container.Get<ICanvasPresenter>(
                            new ConstructorArgument("canvas", canvas),
                            new ConstructorArgument("focus", focus));

                        p.MainPanel.Controls.Add(canvas);
                        p.MouseWheel += (s, e) => canvas.OnZoom(e);
                    };
                });
            
            return container.Get<IMainWindowPresenter>();
        }

        static void Main(string[] args)
        {
            var mainPresenter = CreateMainPresenter();
            
            mainPresenter.ShowView();
        }
    }
}
