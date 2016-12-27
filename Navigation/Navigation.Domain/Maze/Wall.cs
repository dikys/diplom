﻿using System;
using Navigation.Infrastructure;

namespace Navigation.Domain.Maze
{
    public class Wall
    {
        public Line Line { get; }
        public Boolean IsFinish { get; }

        public Wall(Line line, Boolean isFinish = false)
        {
            Line = line;
            IsFinish = isFinish;
        }

        public Wall(Wall wall) : this(wall.Line, wall.IsFinish)
        { }
        
        public Wall Clone()
        {
            return new Wall(this);
        }

        private bool Equals(Wall other)
        {
            return Line.Equals(other.Line) && IsFinish == other.IsFinish;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
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
    }
}
