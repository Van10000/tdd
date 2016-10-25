using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public class Rectangle
    {
        public readonly Point LeftDown;
        public readonly Size Size;

        public Rectangle(Point leftDown, int width, int height) : this(leftDown, new Size(width, height))
        {
        }

        public Rectangle(Point leftDown, Size size)
        {
            LeftDown = leftDown;
            Size = size;
        }

        public bool IntersectsWith(Rectangle rect)
        {
            return Intersects1D(LeftDown.X, rect.LeftDown.X, Size.Width, rect.Size.Width) 
                && Intersects1D(LeftDown.Y, rect.LeftDown.Y, Size.Height, rect.Size.Height);
        }

        private static bool Intersects1D(int firstCoord, int secondCoord, int firstSize, int secondSize)
        {
            return Math.Max(firstCoord, secondCoord) < Math.Min(firstCoord + firstSize, secondCoord + secondSize);
        }

        public IEnumerable<Point> GetPoints()
        {
            for (int i = 0; i <= 1; ++i)
                for (int j = 0; j <= 1; ++j)
                    yield return new Point(LeftDown.X + Size.Width * i, LeftDown.Y + Size.Height * j);
        }
    }
}
