using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigation.Infrastructure
{
    public struct Line
    {
        private Point? _vector;
        private Point? _center;
        private Point? _normilizeVector;
        private double? _length;
        private double? _vectorProductBetweenStartAndEnd;
        
        public Line(double startX, double startY, double endX, double endY)
        {
            Start = new Point(startX, startY);
            End = new Point(endX, endY);
            
            _vector = null;
            _center = null;
            _normilizeVector = null;
            _length = null;
            _vectorProductBetweenStartAndEnd = null;
        }
        public Line(Point start, Point end) : this(start.X, start.Y, end.X, end.Y)
        { }

        public Point Start { get; }
        public Point End { get; }
        public Point Vector => _vector ?? (_vector = End - Start).Value;
        public Point Center => _center ?? (_center = new Point(Start.X + NormilizeVector.X*Length/2, Start.Y + NormilizeVector.Y*Length/2)).Value;
        public Point NormilizeVector => _normilizeVector ?? (_normilizeVector = Vector/Length).Value;
        public double Length => _length ?? (_length = Start.GetDistanceTo(End)).Value;
        public double VectorProductBetweenStartAndEnd => _vectorProductBetweenStartAndEnd ?? (_vectorProductBetweenStartAndEnd = Start.GetVectorProduct(End)).Value;

        #region Основные методы
        public Line Stretch(double coefficient)
        {
            return new Line(Start, End * coefficient);
        }

        public double GetAngleTo(Line other)
        {
            var angle = Math.Acos(NormilizeVector.GetScalarProduct(other.NormilizeVector));

            if (NormilizeVector.GetVectorProduct(other.NormilizeVector) < 0)
                return -angle;

            return angle;
        }
        
        public bool CheckIntersectionPoint(Line other, ref Point intersectionPoint)
        {
            var d1 = (Start - other.Start).GetVectorProduct(other.Vector);
            var d2 = (End - other.Start).GetVectorProduct(other.Vector);
            var d3 = (other.Start - Start).GetVectorProduct(Vector);
            var d4 = (other.End - Start).GetVectorProduct(Vector);

            // d1=d2=d3=d4 = 0 => тогда отрезки имеют множество точек пересечения!

            // тут можно Tollerance использовать
            if (((d1 > InfrastructureConstants.CalculationsAccuracy && d2 < InfrastructureConstants.CalculationsAccuracy)
                 ||
                 (d1 < InfrastructureConstants.CalculationsAccuracy && d2 > InfrastructureConstants.CalculationsAccuracy))
                &&
                ((d3 > InfrastructureConstants.CalculationsAccuracy && d4 < InfrastructureConstants.CalculationsAccuracy)
                 ||
                 (d3 < InfrastructureConstants.CalculationsAccuracy && d4 > InfrastructureConstants.CalculationsAccuracy)))
            {
                var delta = Vector.GetVectorProduct(other.Vector);

                intersectionPoint = new Point(
                    (-VectorProductBetweenStartAndEnd*other.Vector.X +
                     Vector.X*other.VectorProductBetweenStartAndEnd)/delta,
                    (Vector.Y*other.VectorProductBetweenStartAndEnd -
                     other.Vector.Y*VectorProductBetweenStartAndEnd)/delta);

                return true;
            }
            else if (Math.Abs(d1) <= InfrastructureConstants.CalculationsAccuracy && other.CheckOnSegmentStraight(Start))
            {
                intersectionPoint = Start;

                return true;
            }
            else if (Math.Abs(d2) <= InfrastructureConstants.CalculationsAccuracy &&
                     other.CheckOnSegmentStraight(End))
            {
                intersectionPoint = End;

                return true;
            }
            else if (Math.Abs(d3) <= InfrastructureConstants.CalculationsAccuracy &&
                     CheckOnSegmentStraight(other.Start))
            {
                intersectionPoint = other.Start;

                return true;
            }
            else if (Math.Abs(d4) <= InfrastructureConstants.CalculationsAccuracy &&
                     CheckOnSegmentStraight(other.End))
            {
                intersectionPoint = other.End;

                return true;
            }
            else return false;
        }

        /// <summary>
        /// Вернет лежит ли точка point на отрезке, причем point должна лежать на прямой с данный направляющим вектором
        /// </summary>
        public bool CheckOnSegmentStraight(Point point)
        {
            return (Math.Min(Start.X, End.X) - InfrastructureConstants.CalculationsAccuracy <= point.X
                    && point.X <= Math.Max(Start.X, End.X) + InfrastructureConstants.CalculationsAccuracy
                    && Math.Min(Start.Y, End.Y) - InfrastructureConstants.CalculationsAccuracy <= point.Y
                    && point.Y <= Math.Max(Start.Y, End.Y) + InfrastructureConstants.CalculationsAccuracy);
        }

        /// <summary>
        /// Вернет лежит ли точка point на прямой с данным направляющим вектором 
        /// </summary>
        public bool CheckOnStraight(Point point)
        {
            return Math.Abs(Vector.GetVectorProduct(point - Start)) < InfrastructureConstants.CalculationsAccuracy;
        }

        public bool HavePoint(Point point)
        {
            if (!CheckOnStraight(point))
                return false;

            return CheckOnSegmentStraight(point);
        }

        /// <summary>
        /// Вычислить проекцию точки на прямую с направляющим вектором равным этой линии
        /// </summary>
        public Point GetProjectionOnStraight(Point point)
        {
            var t = ((point.X - Start.X)*Vector.X +(point.Y - Start.Y)*Vector.Y) / (Length*Length);

            return new Point(Start.X + Vector.X*t, Start.Y + Vector.Y*t);
        }
        
        public Line Rotate(double angle)
        {
            return new Line(Start, Start + Vector.Rotate(angle));
        }

        public Line Clone()
        {
            return new Line(Start, End);
        }
        #endregion

        #region Перегрузка операторов
        public static Line operator +(Line line, Point point)
        {
            return new Line(line.Start + point, line.End + point);
        }

        public static Line operator -(Line line, Point point)
        {
            return new Line(line.Start - point, line.End - point);
        }
        #endregion

        #region Перегрузка Object методов
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
        
        public override string ToString()
        {
            return "{" + Start.ToString() + ", " + End.ToString() + "}";
        }
        #endregion
    }
}
