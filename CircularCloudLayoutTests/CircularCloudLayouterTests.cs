using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TagsCloudVisualization;
using FluentAssertions;
using System.IO;

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

        [TearDown]
        public void SaveImageIfTestFailed()
        {
            if (TestContext.CurrentContext.Result.FailCount != 0)
            {
                var image = TextPainter.GetPicture(layouter);
                var dirPath =
                    @"C:\Users\ISmir\Desktop\учёба\2 курс\шпора\testing\homework\tdd\CircularCloudLayoutTests\bin\Debug\";
                var fileName = TestContext.CurrentContext.Test.Name + ".png";
                var fullPath = Path.Combine(dirPath, fileName);
                image.Save(fullPath);
                Console.WriteLine("Tag cloud visualization saved to file <{0}>", fullPath);
            }
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
        public void SingleRectangle_SizeTest()
        {
            Rectangle rect = layouter.PutNextRectangle(new Size(1, 2));
            rect.Size.Should().Be(new Size(1, 2));
        }

        [TestCase(1, 1, TestName = "Squares")]
        [TestCase(4, 2, TestName = "Rectangle")]
        public void TwoEqualRectangles_DoNotIntersectTest(int width, int height)
        {
            AssertRectanglesDoNotIntersect(GetSizes(2, width, height));
        }

        [TestCase(5, 2, 3, 7)]
        public void TwoDifferentRectangles_DoNotIntersectTest(int width1, int height1, int width2, int height2)
        {
            AssertRectanglesDoNotIntersect(new Size(width1, height1), new Size(width2, height2));
        }

        [Test]
        public void PutRectangleAtPoint_CorrectRectangleReturnedTest()
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
        public void ManyEqualRectangles_DoNotIntersectTest(int rectanglesNumber, int width, int height)
        {
            AssertRectanglesDoNotIntersect(GetSizes(rectanglesNumber, width, height));
        }

        public void AssertShapeCircle(List<Rectangle> rectangles)
        {
            var sumSpace = rectangles.Sum(rect => rect.Size.Space);
            var minimalCircleRadius = Math.Sqrt(sumSpace / Math.PI);
            var margin = rectangles.Max(rect => Math.Max(rect.Size.Width, rect.Size.Height)) * 2;

            rectangles
                .SelectMany(rect => rect.GetPoints())
                .All(point => point.Distance(new Point(0, 0)) < minimalCircleRadius + margin)
                .Should().BeTrue();
        }

        [TestCase(10, 1, 2)]
        [TestCase(10, 1, 10)]
        [TestCase(100, 13, 3)]
        [TestCase(300, 40, 60)]
        public void ManyRectangles_ShapeCircleTest(int rectanglesNumber, int width, int height)
        {
            var rectangles = AddRectangles(GetSizes(rectanglesNumber, width, height));
            AssertShapeCircle(rectangles);
        }

        private Size GetRandomSize(Random rand, int maxWidth, int maxHeight)
        {
            return new Size(rand.Next() % maxWidth + 1, rand.Next() % maxHeight + 1);
        }

        [TestCase(20, 300, 300, TestName = "Big")]
        [TestCase(30, 5, 20, TestName = "Tall")]
        [TestCase(50, 50, 50, TestName = "Random")]
        [TestCase(200, 20, 5, TestName = "Long")]
        public void ManyRandomRectangles_ShapeCircleTest(int rectanglesNumber, int maxWidth, int maxHeight)
        {
            var rand = new Random();
            var sizes =
                Enumerable.Repeat(new Size(1, 1), rectanglesNumber)
                .Select(_ => GetRandomSize(rand, maxWidth, maxHeight))
                .ToArray();
            var rectangles = AddRectangles(sizes);
            AssertShapeCircle(rectangles);
        }
    }
}
