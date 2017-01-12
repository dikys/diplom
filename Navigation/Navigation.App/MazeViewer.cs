﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigation.App.Windows.Controls;
using Navigation.Domain.Maze;
using Navigation.Domain.Repository;
using Navigation.Infrastructure;

namespace Navigation.App
{
    public class MazeViewer
    {
        private Maze _maze;

        public Line MazeDiameter => _maze.Diameter;

        public MazeViewer(Maze maze)
        {
            _maze = maze;
        }

        public void Draw(Canvas canvas)
        {
            _maze.Walls.ForEach((wall) => canvas.Draw(new Pen(Color.FromArgb(55, 93, 129)), wall));
        }

        public void SetMaze(Maze maze)
        {
            _maze = maze;
        }

        public void SaveMaze(MazeRepository repository, string name)
        {
            repository.Save(_maze, name);
        }

        public static Maze GetDefaultMaze()
        {
            return new Maze(new Wall[]{
                //new Wall(new Line(50, 25, 75, 25)),
                //new Wall(new Line(75, 25, 100, 50)),
                //new Wall(new Line(100, 50, 100, 75)),
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
