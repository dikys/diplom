using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigation.Infrastructure
{
    public struct Point
    {
        public static readonly double Tollerance = 0.01;
        public Point(double x = 0, double y = 0)
        {
            X = x;
            Y = y;
        }
        public Point(Point point) : this(point.X, point.Y)
        { }

        public double X { get; }
        public double Y { get; }

        #region Основные методы
        public double GetDistanceTo(Point point)
        {
            return Math.Sqrt((X - point.X) * (X - point.X) + (Y - point.Y) * (Y - point.Y));
        }

        public double GetScalarProduct(Point point)
        {
            return X*point.X + Y*point.Y;
        }

        public double GetVectorProduct(Point point)
        {
            return X*point.Y - Y*point.X;
        }

        public Point Rotate(double angle)
        {
            return new Point(X*Math.Cos(angle) - Y*Math.Sin(angle), X*Math.Sin(angle) + Y*Math.Cos(angle));
        }

        public double GetNorm() => GetDistanceTo(new Point(0, 0));
        #endregion

        #region Перегрузки операторов
        public static Point operator +(Point left, Point right)
        {
            return new Point(left.X + right.X, left.Y + right.Y);
        }

        public static Point operator -(Point left, Point right)
        {
            return new Point(left.X - right.X, left.Y - right.Y);
        }

        public static Point operator *(Point left, Point right)
        {
            return new Point(left.X * right.X, left.Y * right.Y);
        }

        public static Point operator +(Point left, double coefficient)
        {
            return new Point(left.X + coefficient, left.Y + coefficient);
        }

        public static Point operator -(Point left, double coefficient)
        {
            return new Point(left.X - coefficient, left.Y - coefficient);
        }

        public static Point operator *(Point point, double coefficient)
        {
            return new Point(point.X * coefficient, point.Y * coefficient);
        }

        public static Point operator /(Point point, double coefficient)
        {
            if (Math.Abs(coefficient) < 0.0000001)
                throw new DivideByZeroException("coefficient equals 0");

            return new Point(point.X / coefficient, point.Y / coefficient);
        }
        
        public static bool operator ==(Point left, Point right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !left.Equals(right);
        }
        #endregion

        #region Перегрузки Object методов
        private bool Equals(Point other)
        {
            return (Math.Abs(X - other.X) <= Tollerance) && (Math.Abs(Y - other.Y) <= Tollerance);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = X.GetHashCode();
                hash = hash*17 + Y.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (GetType() != obj.GetType()) return false;

            return Equals((Point)obj);
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";
        }
        #endregion
    }
}
