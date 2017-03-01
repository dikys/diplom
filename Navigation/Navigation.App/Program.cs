using System.Windows.Forms;
using Navigation.App.Windows;
using Ninject;

/*
 *  Планы:
 *     
 *      Интерфейс для настройки робота текущей стратегии и т.д
 *          Настройки констант у Point и Line Tolerance
 *      
 *      Возможность визуального просмотра алгоритма
 *          Наверное, нужно для каждой стратегии создавать StrategyViewer, который все будет выводить
 *          А можно с помощью делегатов сделать
 *      
 *      Написать тесты для
 *          Vision
 *          Maze
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

namespace Navigation.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            Application.Run(new MainWindow());
        }
    }
}
