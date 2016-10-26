using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public static class RectangleExtentions
    {
        public static IEnumerable<Point> GetAllPoints(this IEnumerable<Rectangle> rectangles)
            => rectangles.SelectMany(rect => rect.GetPoints());
    }
}
