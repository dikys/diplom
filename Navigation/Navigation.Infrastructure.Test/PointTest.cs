using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Navigation.Infrastructure.Test
{
    [TestClass]
    public class PointTest
    {
        [TestMethod]
        public void Should_CorrectDistance_When_UsedMethodGetDistanceTo()
        {
            Assert.AreEqual(Math.Sqrt(90*90 + 90*90), new Point(10.5, 10.5).GetDistanceTo(new Point(100.5, 100.5)), Point.Tollerance);
        }

        [TestMethod]
        public void Should_CorrectScalarProduct_When_UsedMethodGetScalarProduct()
        {
            Assert.AreEqual(65, new Point(10, 15).GetScalarProduct(new Point(2, 3)), Point.Tollerance);
        }

        [TestMethod]
        public void Should_CorrectVectorProduct_When_UsedMethodGetVectorProduct()
        {
            Assert.AreEqual(1, new Point(5, 3).GetVectorProduct(new Point(3, 2)), Point.Tollerance);
        }

        [TestMethod]
        public void Should_CorrectVector_When_UsedMethodRotate()
        {
            Assert.AreEqual(new Point(-1, 1), new Point(1, 1).Rotate(Math.PI / 2));
            Assert.AreEqual(new Point(1, -1), new Point(1, 1).Rotate(-Math.PI / 2));
        }
    }
}
