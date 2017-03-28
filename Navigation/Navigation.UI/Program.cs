using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Navigation.UI.Windows;
using Navigation.App.Windows;
using Navigation.App.Views;
using Navigation.App.Extensions;
using Navigation.App.Presenters;
using Navigation.App.Presenters.MainWindow;
using Navigation.App.Presenters.Repository;
using Navigation.Domain.Repository;
using Ninject;

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
            
            // репозиторий
            container.Bind<IRepositoryPresenter>()
                .To<RepositoryPresenter>()
                .InSingletonScope();
            container.Bind<IRepositoryView>()
                .To<RepositoryWindow>()
                .InSingletonScope();
            container.Bind<IMazeRepository>()
                .To<MazeRepository>()
                .InSingletonScope()
                .WithConstructorArgument("path", "mazes/");

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

            /*//создаем контроллеры
            var repositoryPresenter = container.Get<RepositoryPresenter>();
            
            var window = new BaseWindow();
            window.TopMenuStrip.WithItems(
                new ToolStripButton("Открыть Репозиторий")
                .WithToolTipText("Репозиторий")
                .WithOnClick((s, e) =>
                {
                    repositoryPresenter.ShowView();
                }));*/

            return container.Get<IMainWindowPresenter>();
        }

        static void Main(string[] args)
        {
            var mainPresenter = CreateMainPresenter();
            
            mainPresenter.ShowView();
        }
    }
}
