using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Navigation.Infrastructure.Test
{
    [TestClass]
    public class PointTest
    {
        [TestMethod]
        public void Should_CorrectDistance()
        {
            Assert.AreEqual(Math.Sqrt(90*90 + 90*90), new Point(10.5, 10.5).GetDistanceTo(new Point(100.5, 100.5)), Point.Tollerance);
        }

        [TestMethod]

    }
}
