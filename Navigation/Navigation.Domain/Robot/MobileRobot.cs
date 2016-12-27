using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigation.Infrastructure;

namespace Navigation.Domain.Robot
{
    public abstract class MobileRobot
    {
        public Point Position { get; protected set; }
        public Vision Vision { get; }

        public double Size { get; }

        protected MobileRobot(Maze.Maze maze, Point position)
        {
            Position = position;
            Vision = new Vision(maze, this);

            Size = 5;
        }

        public abstract void Run();

        /// <summary>
        /// Причем концы отрезков, представляющие контур, должны идти в порядке их обхода против часовой
        /// </summary>
        /// <param name="contour"></param>
        /// <returns></returns>
        protected List<Line> GetPassageInСontour(List<Line> contour)
        {
            var result = new List<Line>();

            for (var index = 0; index < contour.Count - 1; index++)
            {
                if(contour[index].End.GetDistanceTo(contour[index + 1].Start) > Size)
                    result.Add(new Line(contour[index].End, contour[index + 1].Start));
            }

            if (contour[contour.Count - 1].End.GetDistanceTo(contour[0].Start) > Size)
                result.Add(new Line(contour[contour.Count - 1].End, contour[0].Start));

            return result;
        }
    }
}
