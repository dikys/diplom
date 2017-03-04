using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigation.Domain.Exceptions
{
    public class MazeHaveGapException : Exception
    {
        public MazeHaveGapException(Exception innerException)
            : base("", innerException)
        { }

        public MazeHaveGapException()
            : base(null)
        { }
    }
}
