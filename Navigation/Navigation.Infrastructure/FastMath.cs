using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigation.Infrastructure
{
    public class FastMath
    {
        private List<double> _sinTable;
        private List<double> _cosTable;

        public FastMath()
        {
            _sinTable = new List<double>();
            _cosTable = new List<double>();

            var numberStep = 131072;
            var angleStep = (Math.PI*2)/numberStep;
            var angle = 0D;

            do
            {
                _sinTable.Add(Math.Sin(angle));
                _cosTable.Add(Math.Cos(angle));

                angle += angleStep;
            } while (angle < Math.PI*2);
        }

        public double FastSin(double angle)
        {
            return _sinTable[(int) (angle/_sinTable.Count) & 131071];
        }

        public double FastCos(double angle)
        {
            return _cosTable[(int) (angle/_cosTable.Count) & 131071];
        }
    }
}