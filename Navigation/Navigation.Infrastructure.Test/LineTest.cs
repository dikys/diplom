using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Navigation.Infrastructure.Test
{
    [TestClass]
    public class LineTest
    {
        [TestMethod]
        public void Should_CorrectLine_When_UsedMethodStretch()
        {
            Assert.AreEqual(new Line(10, 10, 300, 150), new Line(10, 10, 100, 50).Stretch(3));
        }

        [TestMethod]
        public void Should_CorrectAngle_When_UsedMethodGetAngleTo()
        {
            Assert.AreEqual(-Math.PI / 4, new Line(1, 1, 10, 10).GetAngleTo(new Line(100, 33, 150, 33)));
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

        [TestMethod]
        public void Should_CorrectResult_When_UsedMethodOnSegmentStraight()
        {
            Assert.IsTrue(new Line(0, 0, 100, 0).OnSegmentStraight(new Point(100, 0)));
            Assert.IsFalse(new Line(0, 0, -100, -100).OnSegmentStraight(new Point(-101, -101)));
        }

        [TestMethod]
        public void Should_CorrectResult_When_UsedMethodOnStraight()
        {
            Assert.IsTrue(new Line(0, 0, -100, -100).OnStraight(new Point(-101, -101)));
            Assert.IsFalse(new Line(0, 0, -100, -100).OnStraight(new Point(0, 2)));
        }

        [TestMethod]
        public void Should_CorrectResult_When_UsedMethodHavePoint()
        {
            Assert.IsTrue(new Line(0, 0, -100, -100).HavePoint(new Point(-100, -100)));
            Assert.IsFalse(new Line(0, 0, -100, -100).HavePoint(new Point(-101, -101)));
        }

        [TestMethod]
        public void Should_CorrectProjection_When_UsedMethodGetProjectionOnStraight()
        {
            Assert.AreEqual(new Point(1, 2), new Line(-2, 0, -5, -2).GetProjectionOnStraight(new Point(-1, 5)));
        }

        [TestMethod]
        public void Should_CorrectLine_When_UsedMethodRotate()
        {
            Assert.AreEqual(new Line(100, 50, 50, 50), new Line(100, 50, 100, 0).Rotate(-Math.PI / 2));
        }
    }
}
