using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
