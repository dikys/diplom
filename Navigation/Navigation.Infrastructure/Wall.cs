﻿namespace Navigation.Infrastructure
{
    public struct Wall
    {
        public Line Line { get; }
        public bool IsFinish { get; }
        
        public Wall(Line line, bool isFinish = false)
        {
            Line = line;
            IsFinish = isFinish;
        }

        public Wall(Wall wall) : this(wall.Line, wall.IsFinish)
        { }

        #region Перегрузка Object методов
        private bool Equals(Wall other)
        {
            return Line.Equals(other.Line) && IsFinish == other.IsFinish;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            // ReSharper disable once ReferenceEqualsWithValueType
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            return Equals((Wall)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = Line.GetHashCode();
                hash = hash*37 + Line.GetHashCode();
                hash = hash*37 + (IsFinish ? 1 : 0);

                return hash;
            }
        }

        public override string ToString()
        {
            return "Wall[" + Line.ToString() + ", " + IsFinish + "]";
        }
        #endregion
    }
}
