using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TagsCloudVisualization;
using FluentAssertions;

namespace CurcularCloudLayouterTests
{
    [TestFixture]
    class CircularCloudLayouterTests
    {
        private CircularCloudLayouter layouter;

        [SetUp]
        public void CreateLayouter()
        {
            layouter = new CircularCloudLayouter(new Point(0, 0));
        }

        private Size[] GetSizes(int rectanglesNumber, int width, int height)
        {
            return Enumerable.Repeat(new Size(width, height), rectanglesNumber).ToArray();
        }

        private List<Rectangle> AddRectangles(params Size[] sizes)
        {
            return sizes.Select(t => layouter.PutNextRectangle(t)).ToList();
        }

        private void AssertRectanglesDoNotIntersect(params Size[] sizes)
        {
            var rectangles = AddRectangles(sizes);

            for (int i = 0; i < sizes.Length; ++i)
                for (int j = i + 1; j < sizes.Length; ++j)
                    rectangles[i].IntersectsWith(rectangles[j]).Should().BeFalse();
        }
        
        [Test]
        public void SingleRectangleSizeTest()
        {
            Rectangle rect = layouter.PutNextRectangle(new Size(1, 2));
            rect.Size.Should().Be(new Size(1, 2));
        }

        [TestCase(1, 1, TestName = "Squares")]
        [TestCase(4, 2, TestName = "Rectangle")]
        public void TwoEqualRectanglesDoNotIntersectTest(int width, int height)
        {
            AssertRectanglesDoNotIntersect(GetSizes(2, width, height));
        }

        [TestCase(5, 2, 3, 7)]
        public void TwoDifferentRectanglesDoNotIntersectTest(int width1, int height1, int width2, int height2)
        {
            AssertRectanglesDoNotIntersect(new Size(width1, height1), new Size(width2, height2));
        }

        [Test]
        public void PutRectangleAtPointTest()
        {
            var point = new Point(0, 0);
            var size = new Size(1, 1);
            var rect = layouter.PutRectangleAtPoint(point, size);
            rect.GetPoints().Contains(point).Should().BeTrue();
            rect.Size.Should().Be(size);
        }
        
        [TestCase(3, 1, 1)]
        [TestCase(5, 1, 2)]
        [TestCase(100, 5, 7)]
        public void ManyEqualRectanglesDoNotIntersectTest(int rectanglesNumber, int width, int height)
        {
            AssertRectanglesDoNotIntersect(GetSizes(rectanglesNumber, width, height));
        }

        [TestCase(10, 1, 2)]
        [TestCase(10, 1, 10)]
        [TestCase(100, 13, 3)]
        public void ManyRectanglesPlacedInCircleTest(int rectanglesNumber, int width, int height)
        {
            var rectangles = AddRectangles(GetSizes(rectanglesNumber, width, height));
            var sumSpace = rectanglesNumber * width * height;
            var minimalCircleRadius = Math.Sqrt(sumSpace / Math.PI);
            var margin = Math.Max(width, height) * 2;

            rectangles
                .SelectMany(rect => rect.GetPoints())
                .All(point => point.Distance(new Point(0, 0)) < minimalCircleRadius + margin)
                .Should().BeTrue();
        }
    }
}
