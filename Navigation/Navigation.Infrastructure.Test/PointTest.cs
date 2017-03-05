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
            Assert.AreEqual(Math.Sqrt(90*90 + 90*90), new Point(10.5, 10.5).GetDistanceTo(new Point(100.5, 100.5)), InfrastructureConstants.CalculationsAccuracy);
        }

        [TestMethod]
        public void Should_CorrectScalarProduct_When_UsedMethodGetScalarProduct()
        {
            Assert.AreEqual(65, new Point(10, 15).GetScalarProduct(new Point(2, 3)), InfrastructureConstants.CalculationsAccuracy);
        }

        [TestMethod]
        public void Should_CorrectVectorProduct_When_UsedMethodGetVectorProduct()
        {
            Assert.AreEqual(1, new Point(5, 3).GetVectorProduct(new Point(3, 2)), InfrastructureConstants.CalculationsAccuracy);
        }

        [TestMethod]
        public void Should_CorrectVector_When_UsedMethodRotate()
        {
            Assert.AreEqual(new Point(-1, 1), new Point(1, 1).Rotate(Math.PI / 2));
            Assert.AreEqual(new Point(1, -1), new Point(1, 1).Rotate(-Math.PI / 2));
        }

        [TestMethod]
        public void Should_CorrectOperatorOverloading()
        {
            var p1 = new Point(100, 100);
            var p2 = new Point(50, 50);

            Assert.AreEqual(new Point(150, 150), p1 + p2);
            Assert.AreEqual(new Point(-50, -50), p2 - p1);
            Assert.AreEqual(new Point(5000, 5000), p1*p2);

            Assert.AreEqual(new Point(103, 103), p1 + 3);
            Assert.AreEqual(new Point(97, 97), p2 + 47);
            Assert.AreEqual(new Point(300, 300), p1*3);
            Assert.AreEqual(new Point(25, 25), p1/4);

            Assert.IsFalse(p1 == p2);
            Assert.IsTrue(p1 != p2);
        }
    }
}
