using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public class InvalidArgumentParserException : ApplicationException
    {
        #region Construction

        public InvalidArgumentParserException() { }

        public InvalidArgumentParserException(string message) : base(message) { }

        public InvalidArgumentParserException(string message, Exception innerException)
            : base(message, innerException) { }

        #endregion
    }
}