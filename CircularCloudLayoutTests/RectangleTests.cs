using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace CircularCloudLayoutTests
{
    [TestFixture]
    internal class RectangleTests
    {
        [Test]
        public void NewRectangle_CorrectFieldsTest()
        {
            var rect = new Rectangle(new Point(2, 3), 5, 1);
            rect.LeftDown.Should().Be(new Point(2, 3));
            rect.Size.Should().Be(new Size(5, 1));
        }

        [Test, TestCaseSource(typeof(RectanglesIntersectionTestData), nameof(RectanglesIntersectionTestData.TestCases))]
        public bool Rectangles_IntersectionTest(Rectangle first, Rectangle second)
        {
            return first.IntersectsWith(second);
        }

        [Test]
        public void GetPoints_CorrectResultTest()
        {
            var rect = new Rectangle(new Point(0, 0), 2, 4);
            rect.GetPoints()
                .ShouldAllBeEquivalentTo(new[] {new Point(0, 0), new Point(2, 0), new Point(0, 4), new Point(2, 4)});
        }
    }

    class RectanglesIntersectionTestData
    {
        public static IEnumerable TestCases()
        {
            yield return new TestCaseData(
                new Rectangle(new Point(0, 0), 2, 2),
                new Rectangle(new Point(1, 1), 2, 2))
                .Returns(true)
                .SetName("Simple intersection");

            yield return new TestCaseData(
                new Rectangle(new Point(0, 0), 1, 2),
                new Rectangle(new Point(2, 0), 1, 1))
                .Returns(false)
                .SetName("Same Y, but don't intersect.");

            yield return new TestCaseData(
                new Rectangle(new Point(0, 0), 1, 3),
                new Rectangle(new Point(0, 4), 1, 3))
                .Returns(false)
                .SetName("Same X, but don't intersect.");

            yield return new TestCaseData(
                new Rectangle(new Point(0, 0), 1, 4),
                new Rectangle(new Point(0, 4), 1, 4))
                .Returns(false)
                .SetName("Common line, don't intersect.");

            yield return new TestCaseData(
                new Rectangle(new Point(0, 0), 1, 5),
                new Rectangle(new Point(0, 4), 1, 1))
                .Returns(true)
                .SetName("Minimal overlap intersection.");

            yield return new TestCaseData(
                new Rectangle(new Point(0, 0), 4, 4),
                new Rectangle(new Point(1, 1), 2, 2))
                .Returns(true)
                .SetName("One inside another");
        }
    }
}
