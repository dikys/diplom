using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Navigation.Infrastructure
{
    public static class InfrastructureConstants
    {
        public static double CalculationsAccuracy { get; }

        static InfrastructureConstants()
        {
            CalculationsAccuracy = 0.000001;
        }
    }
}
