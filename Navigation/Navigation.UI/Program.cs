﻿using System.Windows.Forms;
using Navigation.UI.Windows;

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
 *          DefaultRobotVision
 *          DefaultMaze
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
        static void Main(string[] args)
        {
            Application.Run(new MainWindow());
        }
    }
}