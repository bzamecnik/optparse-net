using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public class ParseException : ApplicationException
    {
        #region Construction

        public ParseException() { }

        public ParseException(string message) : base(message) { }

        public ParseException(string message, Exception innerException)
            : base(message, innerException) { }

        #endregion
    }
}
