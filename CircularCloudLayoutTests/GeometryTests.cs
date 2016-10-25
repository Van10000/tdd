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
    public class GeometryTests
    {
        [Test]
        public void NewPointFieldsTest()
        {
            var point = new Point(3, 5);
            point.X.Should().Be(3);
            point.Y.Should().Be(5);
        }

        [Test]
        public void NewSizeFieldsTest()
        {
            var size = new Size(10, 15);
            size.Width.Should().Be(10);
            size.Height.Should().Be(15);
        }

        [Test]
        public void NegativeSizeTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Size(-1, 0));
        }

        [Test]
        public void ZeroSizeTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Size(0, 0));
        }
    }
}
