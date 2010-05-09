using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    /// <summary>
    /// The exception that is thrown when an the command line arguments
    /// do not conform to the rules defined by the options.
    /// </summary>
    public class ParseException : ApplicationException
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="ParseException"/>
        /// class.
        /// </summary>
        public ParseException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParseException"/>
        /// class with a specified error message.
        /// </summary>
        public ParseException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParseException"/>
        /// class with a specified error message and a reference to the inner
        /// exception that caused this exception.
        /// </summary>
        public ParseException(string message, Exception innerException)
            : base(message, innerException) { }

        #endregion
    }
}
