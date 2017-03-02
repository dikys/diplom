using System.Drawing;
using Navigation.App.Repository;
using Navigation.App.Windows.Controls;
using Navigation.Domain.Mazes;
using Navigation.Infrastructure;
using Navigation.UI.Windows.Controls;

namespace Navigation.UI
{
    public class MazeViewer
    {
        public IMaze Maze;

        public MazeViewer(IMaze maze)
        {
            Maze = maze;
        }

        public void Draw(Canvas canvas)
        {
            Maze.Walls.ForEach((wall) => canvas.Draw(new Pen(Color.FromArgb(55, 93, 129)), wall));
        }

        public void SaveMaze(MazeRepository repository, string name)
        {
            repository.Save(Maze, name);
        }

        public static IMaze GetDefaultMaze()
        {
            return new DefaultMaze(new Wall[]{
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
