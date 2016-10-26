﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        private readonly Point center;
        private readonly List<Rectangle> previousRectangles = new List<Rectangle>();

        public ReadOnlyCollection<Rectangle> PreviousRectangles => previousRectangles.AsReadOnly();
        public Point Center => new Point(center.X, center.Y);

        public CircularCloudLayouter(Point center)
        {
            this.center = center;
        }

        private Rectangle AddRectangle(Rectangle rectangle)
        {
            previousRectangles.Add(rectangle);
            return rectangle;
        }

        public Rectangle PutNextRectangle(Size rectangleSze)
        {
            if (previousRectangles.Count == 0)
                return AddRectangle(new Rectangle(center, rectangleSze));
            var resultRect = previousRectangles
                .GetAllPoints()
                .Select(point => PutRectangleAtPoint(point, rectangleSze))
                .Where(newRect => newRect != null)
                .OrderBy(newRect => newRect
                                    .GetPoints()
                                    .Min(point => center.Distance(point)))
                .FirstOrDefault();
            return resultRect != null ? AddRectangle(resultRect) : null;
        }

        private bool CanBeAdded(Rectangle rect)
        {
            return !previousRectangles.Any(rect.IntersectsWith);
        }

        public Rectangle PutRectangleAtPoint(Point point, Size rectangleSize)
        {
            for (var i = 0; i <= 1; ++i)
                for (var j = 0; j <= 1; ++j)
                {
                    var curX = point.X - rectangleSize.Width * i;
                    var curY = point.Y - rectangleSize.Height * j;
                    var curRect = new Rectangle(new Point(curX, curY), rectangleSize);
                    if (CanBeAdded(curRect))
                        return curRect;
                }
            return null;
        }
    }
}
