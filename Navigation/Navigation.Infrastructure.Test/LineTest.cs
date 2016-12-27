using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Navigation.Infrastructure.Test
{
    [TestClass]
    public class LineTest
    {
        [TestMethod]
        public void Should_CorrectIntersectionPoint_When_UseHaveIntersectionPoint()
        {
            var intersectionPoint = new Point();

            Assert.IsTrue(new Line(-1, -1, 1, 1).HaveIntersectionPoint(new Line(-1, 1, 1, -1), ref intersectionPoint));
            Assert.AreEqual(new Point(0, 0), intersectionPoint);

            Assert.IsTrue(new Line(-1, 2, 0, 1).HaveIntersectionPoint(new Line(-1, 1, 0, 2), ref intersectionPoint));
            Assert.AreEqual(new Point(-0.5, 1.5), intersectionPoint);

            Assert.IsTrue(new Line(50, 50, 130, 50).HaveIntersectionPoint(new Line(100, 50, 100, 75), ref intersectionPoint));
            Assert.AreEqual(new Point(100, 50), intersectionPoint);
        }
    }
}
