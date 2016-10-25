using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace CurcularCloudLayouterTests
{
    [TestFixture]
    class RectangleTests
    {
        void AssertRectanglesIntersect(Rectangle rect1, Rectangle rect2, bool intersect)
        {
            rect1.IntersectsWith(rect2).Should().Be(intersect);
        }

        [Test]
        public void NewRectangleFieldsTest()
        {
            var rect = new Rectangle(new Point(2, 3), 5, 1);
            rect.LeftDown.Should().Be(new Point(2, 3));
            rect.Size.Should().Be(new Size(5, 1));
        }

        [TestCase(
            0, 0, 2, 2,
            1, 1, 2, 2,
            ExpectedResult = true,
            TestName = "Simple intersection")]
        [TestCase(
            0, 0, 1, 1,
            2, 0, 1, 1,
            ExpectedResult = false,
            TestName = "Same Y, but don't intersect.")]
        [TestCase(
            0, 0, 1, 3,
            0, 4, 1, 3,
            ExpectedResult = false,
            TestName = "Same X, but don't intersect.")]
        [TestCase(
            0, 0, 1, 4,
            0, 4, 1, 4,
            ExpectedResult = false,
            TestName = "Common line, don't intersect.")]
        [TestCase(
            0, 0, 1, 5,
            0, 4, 1, 1,
            ExpectedResult = true,
            TestName = "Minimal overlap intersection.")]
        [TestCase(
            0, 0, 4, 4,
            1, 1, 2, 2,
            ExpectedResult = true,
            TestName = "One inside another")]
        public bool RectanglesIntersectTest(int p1X, int p1Y, int p1Width, int p1Height, int p2X, int p2Y, int p2Width,
            int p2Height)
        {
            var rect1 = new Rectangle(new Point(p1X, p1Y), p1Width, p1Height);
            var rect2 = new Rectangle(new Point(p2X, p2Y), p2Width, p2Height);
            return rect1.IntersectsWith(rect2);
        }

        [Test]
        public void GetPointsTest()
        {
            var rect = new Rectangle(new Point(0, 0), 2, 4);
            rect.GetPoints()
                .ShouldAllBeEquivalentTo(new[] {new Point(0, 0), new Point(2, 0), new Point(0, 4), new Point(2, 4)});
        }
    }
}
