using Navigation.Infrastructure;
using Newtonsoft.Json;

namespace Navigation.Domain.Repository.Representations
{
    internal class LineRepresentation
    {
        public PointRepresentation Start;
        public PointRepresentation End;

        [JsonConstructor]
        public LineRepresentation(PointRepresentation start, PointRepresentation end)
        {
            Start = start;
            End = end;
        }

        public LineRepresentation(Line line) : this(new PointRepresentation(line.Start), new PointRepresentation(line.End)) { }

        public static explicit operator Line(LineRepresentation lineRepresentation)
        {
            return new Line((Point)lineRepresentation.Start, (Point)lineRepresentation.End);
        }
    }
}
