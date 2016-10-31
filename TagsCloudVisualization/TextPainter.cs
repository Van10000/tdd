using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public static class TextPainter
    {
        private const double MinimalRelativeMargin = 0.2;
        private const int MinimalAbsoluteMargin = 5;

        public static Bitmap GetPicture(CircularCloudLayouter layouter)
        {
            var rectangles = layouter.PreviousRectangles;
            var width = FindIntervalWithMargin(rectangles, p => p.X);
            var height = FindIntervalWithMargin(rectangles, p => p.Y);

            var bitmap = new Bitmap(width, height);
            var graphics = Graphics.FromImage(bitmap);
            var shift = new Point(width / 2, height / 2) - layouter.Center;
            graphics.FillRegion(Brushes.White, new Region(new System.Drawing.Rectangle(0, 0, width, height)));
            var rand = new Random();
            foreach (var rect in rectangles)
            {
                var rectToPaint = ToDrawingRectangle(rect, shift);
                var color = GetRandomColor(rand);
                graphics.FillRectangle(new SolidBrush(color), rectToPaint);
            }
            return bitmap;
        }

        private static int FindIntervalWithMargin(IList<Rectangle> rectangles, Func<Point, int> keySelector)
        {
            var interval = rectangles.GetAllPoints().Max(keySelector) - rectangles.GetAllPoints().Min(keySelector);
            var withRelativeMargin = (int) (interval * (1 + MinimalRelativeMargin));
            var withAbloluteMargin = interval + MinimalAbsoluteMargin;
            return Math.Max(withRelativeMargin, withAbloluteMargin);
        }

        private static System.Drawing.Rectangle ToDrawingRectangle(Rectangle rect, Point shift)
        {
            return new System.Drawing.Rectangle(
                rect.LeftDown.X + shift.X,
                rect.LeftDown.Y + shift.Y,
                rect.Size.Width,
                rect.Size.Height);
        }

        private static Color GetRandomColor(Random rand)
            => Color.FromArgb(127, rand.Next(100, 255), rand.Next(100, 255), rand.Next(100, 255));
    }
}
