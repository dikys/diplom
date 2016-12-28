﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Navigation.Infrastructure;

/*
 *  Планы:
 *      
 *      Написать тест на HaveIntersection
 *      
 *      Реализовать прошлую стратегию
 *  
 *      Интерфейс для настройки робота текущей стратегии и т.д
 *          Настройки констант у Point и Line Tolerance
 *      
 *      Возможность визуального просмотра алгоритма
 *          Наверное, нужно для каждой стратегии создавать StrategyViewer, который все будет выводить
 *      
 *      Написать тесты для
 *          Point
 *          Line
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