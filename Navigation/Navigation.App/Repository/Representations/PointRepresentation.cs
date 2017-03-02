using Navigation.Infrastructure;
using Newtonsoft.Json;

namespace Navigation.App.Repository.Representations
{
    internal class PointRepresentation
    {
        public double X;
        public double Y;

        [JsonConstructor]
        public PointRepresentation(double x, double y)
        {
            X = x;
            Y = y;
        }

        public PointRepresentation(Point point) : this(point.X, point.Y) { }

        public static explicit operator Point(PointRepresentation pointRepresentation)
        {
            return new Point(pointRepresentation.X, pointRepresentation.Y);
        }
    }
}
