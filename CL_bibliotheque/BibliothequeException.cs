using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL_Utils
{
    public class BibliothequeException : Exception
    {
        public BibliothequeException() { }

        public BibliothequeException(string message) : base(message) { }

        public BibliothequeException(string message, Exception innerException) : base(message, innerException) { }
    }

}

