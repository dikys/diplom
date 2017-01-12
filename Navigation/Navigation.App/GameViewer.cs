using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Navigation.App.Extensions;
using Navigation.App.Windows.Controls;
using Navigation.Domain.Maze;
using Navigation.Domain.Robot;
using Navigation.Domain.Strategies.DFS;
using Navigation.Infrastructure;
using Newtonsoft.Json;
using Point = Navigation.Infrastructure.Point;

namespace Navigation.App
{
    public class GameViewer
    {
        public Line MazeDiameter => _maze.Diameter;

        private Maze _maze;
        private MobileRobot _robot;

        private Canvas _canvas;

        public GameViewer()
        {
            _maze = GetDefaultMaze();
            _robot = new RobotWithDFS(_maze, new Point(50, 50));
        }

        public void RunRobot()
        {
            _robot.Run();
        }

        public void Save(string path)
        {
            var str = JsonConvert.SerializeObject(_maze);

            File.WriteAllText(path, str);
        }

        public void Load(string path)
        {
            var str = File.ReadAllText(path);
            
            _maze = JsonConvert.DeserializeObject<Maze>(str);
        }

        public void Draw(Canvas canvas)
        {
            _maze.Walls.ForEach((wall) => canvas.Draw(new Pen(Color.FromArgb(55, 93, 129)), wall));

            canvas.Draw(Brushes.LawnGreen, _robot.Position);
        }

        private Maze GetDefaultMaze()
        {
            return new Maze(new Wall[]{
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
