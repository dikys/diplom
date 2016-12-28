using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Navigation.Infrastructure.Test
{
    [TestClass]
    public class LineTest
    {
        public void Should_CorrectLine_When_UsedMethodStretch()
        {
            
        }

        [TestMethod]
        public void Should_CorrectIntersectionPoint_When_UsedMethodHaveIntersectionPoint()
        {
            var intersectionPoint = new Point();

            Assert.IsTrue(new Line(-1, -1, 1, 1).HaveIntersectionPoint(new Line(-1, 1, 1, -1), ref intersectionPoint));
            Assert.AreEqual(new Point(0, 0), intersectionPoint);

            Assert.IsTrue(new Line(-1, 2, 0, 1).HaveIntersectionPoint(new Line(-1, 1, 0, 2), ref intersectionPoint));
            Assert.AreEqual(new Point(-0.5, 1.5), intersectionPoint);

            Assert.IsTrue(new Line(50, 50, 130, 50).HaveIntersectionPoint(new Line(100, 50, 100, 75), ref intersectionPoint));
            Assert.AreEqual(new Point(100, 50), intersectionPoint);
        }

        [TestMethod]
        public void Should_ResultFalse_When_LinesDoNotIntersect()
        {
            var intersectionPoint = new Point();

            Assert.IsFalse(new Line(1, 0, 50, 0).HaveIntersectionPoint(new Line(-10, -1, 30, -1), ref intersectionPoint));
            Assert.IsFalse(new Line(0, 0, 20, 20).HaveIntersectionPoint(new Line(21, 21, 30, 5), ref intersectionPoint));
        }
    }
}
