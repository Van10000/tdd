using System;
using System.Diagnostics.CodeAnalysis;

namespace TagsCloudVisualization
{
    public class Point
    {
        public readonly int X, Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public double Distance(Point point)
        {
            var XProj = point.X - X;
            var YProj = point.Y - Y;
            return Math.Sqrt(XProj * XProj + YProj * YProj);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Point) obj);
        }

        protected bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }

        public static Point operator -(Point a, Size b)
        {
            return new Point(a.X - b.Width, a.Y - b.Height);
        }
    }
}
