using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigation.Infrastructure
{
    public class Line
    {
        private static readonly double Tollerance = 0.01;

        public Point Start { get; }
        public Point End { get; }
        
        private Point? _vector;
        public Point Vector
        {
            get
            {
                if (!_vector.HasValue)
                    _vector = new Point(End.X - Start.X, End.Y - Start.Y);

                return _vector.Value;
            }
        }

        private Point? _center;
        public Point Center
        {
            get
            {
                if (!_center.HasValue)
                    _center = new Point(Start.X + NormilizeVector.X * Length / 2, Start.Y + NormilizeVector.Y * Length / 2);

                return _center.Value;
            }
        }

        private Point? _normilizeVector;
        public Point NormilizeVector
        {
            get
            {
                if (!_normilizeVector.HasValue)
                    _normilizeVector = Vector / Length;

                return _normilizeVector.Value;
            }
        }

        private double? _length;
        public double Length
        {
            get
            {
                if (!_length.HasValue)
                    _length = Start.GetDistanceTo(End);

                return _length.Value;
            }
        }

        /*private double? _vectorProductBetweenStartAndEnd;
        public double VectorProductBetweenStartAndEnd
        {
            get
            {
                if (!_vectorProductBetweenStartAndEnd.HasValue)
                    _vectorProductBetweenStartAndEnd = Start.GetVectorProduct(End);

                return _vectorProductBetweenStartAndEnd.Value;
            }
        }*/

        public Line(double startX, double startY, double endX, double endY)
        {
            Start = new Point(startX, startY);
            End = new Point(endX, endY);
        }

        public Line(Point start, Point end) : this(start.X, start.Y, end.X, end.Y)
        { }

        public Line Stretch(double coefficient)
        {
            return new Line(Start, End * coefficient);
        }

        public double GetAngleTo(Line other)
        {
            var cosOfAngle = NormilizeVector.GetScalarProduct(other.NormilizeVector);
            var sinOfAngle = NormilizeVector.GetVectorProduct(other.NormilizeVector);

            var angle = Math.Acos(cosOfAngle);

            if (sinOfAngle < 0)
                return -angle;

            return angle;
        }
        
        

        public bool HaveIntersectionPoint(Line other, ref Point intersectionPoint)
        {
            var d1 = (Start - other.Start).GetVectorProduct(other.Vector);
            var d2 = (End - other.Start).GetVectorProduct(other.Vector);
            var d3 = (other.Start - Start).GetVectorProduct(Vector);
            var d4 = (other.End - Start).GetVectorProduct(Vector);

            // d1=d2=d3=d4 = 0 => тогда отрезки имеют множество точек пересечения!

            if (((d1 > 0 && d2 < 0) || (d1 < 0 && d2 > 0))
                && ((d3 > 0 && d4 < 0) || (d3 < 0 && d4 > 0)))
            {
                /*var delta = -Vector.Y*other.Vector.X + other.Vector.Y*Vector.X;
                var delta1 = -Start.GetVectorProduct(End)*other.Vector.X + other.Start.GetVectorProduct(End)*Vector.X;
                var delta2 = Vector.Y*other.Start.GetVectorProduct(End) - other.Vector.Y*Start.GetVectorProduct(End);

                intersectionPoint = new Point(delta1 / delta, delta2 / delta);*/

                var delta = Vector.GetVectorProduct(other.Vector);
                var deltaX = -Start.GetVectorProduct(End)*other.Vector.X +
                             Vector.X*other.Start.GetVectorProduct(other.End);
                var deltaY = Vector.Y*other.Start.GetVectorProduct(other.End) -
                             other.Vector.Y*Start.GetVectorProduct(End);

                intersectionPoint = new Point(deltaX/delta, deltaY/delta);

                return true;
            }
            else if (d1 == 0 && other.OnSegmentStraight(Start))
            {
                intersectionPoint = Start.Clone();

                return true;
            }
            else if (d2 == 0 && other.OnSegmentStraight(End))
            {
                intersectionPoint = End.Clone();

                return true;
            }
            else if (d3 == 0 && OnSegmentStraight(other.Start))
            {
                intersectionPoint = other.Start.Clone();

                return true;
            }
            else if (d4 == 0 && OnSegmentStraight(other.End))
            {
                intersectionPoint = other.End.Clone();

                return true;
            }
            else return false;
        }

        /// <summary>
        /// Вернет лежит ли точка point на отрезке, причем point должна лежать на прямой с данный направляющим вектором
        /// </summary>
        public bool OnSegmentStraight(Point point)
        {
            return (Math.Min(Start.X, End.X) - Tollerance <= point.X && point.X <= Math.Max(Start.X, End.X) + Tollerance
                    && Math.Min(Start.Y, End.Y) - Tollerance <= point.Y && point.Y <= Math.Max(Start.Y, End.Y) + Tollerance);
        }

        /// <summary>
        /// Вычислить проекцию точки на прямую с направляющим вектором равным этой линии
        /// </summary>
        public Point GetProjectionOnStraight(Point point)
        {
            var t = ((point.X - Start.X)*Vector.X +(point.Y - Start.Y)*Vector.Y) / (Length*Length);

            return new Point(Start.X + Vector.X*t, Start.Y + Vector.Y*t);
        }

        public bool HavePoint(Point point)
        {
            //return Math.Abs(Start.GetDistanceTo(point) + point.GetDistanceTo(End) - Length) < Tollerance;
            if (Math.Abs(Vector.GetVectorProduct(point - Start)) > Tollerance)
                return false;

            return OnSegmentStraight(point);
        }

        public Line Rotate(double angle)
        {
            return new Line(Start, Start + Vector.Rotate(angle));
        }

        public static Line operator +(Line line, Point point)
        {
            return new Line(line.Start + point, line.End + point);
        }

        public static Line operator -(Line line, Point point)
        {
            return new Line(line.Start - point, line.End - point);
        }

        /*public static bool operator ==(Line left, object right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Line left, object right)
        {
            return !left.Equals(right);
        }*/

        private bool Equals(Line other)
        {
            return Start == other.Start && End == other.End;
                   //|| Start == other.End && End == other.Start;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            return Equals((Line)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = Start.GetHashCode();
                hash = hash*37 + End.GetHashCode();

                return hash;
            }
        }

        public Line Clone()
        {
            return new Line(Start, End);
        }

        public override string ToString()
        {
            return "{" + Start.ToString() + ", " + End.ToString() + "}";
        }
    }
}
