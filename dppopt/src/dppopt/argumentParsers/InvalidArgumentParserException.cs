using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    /// <summary>
    /// The exception that is thrown when an argument parser for a type
    /// is missing or is not applicable for that type.
    /// </summary>
    public class InvalidArgumentParserException : ApplicationException
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="InvalidArgumentParserException"/> class.
        /// </summary>
        public InvalidArgumentParserException() { }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="InvalidArgumentParserException"/> class with a
        /// specified error message.
        /// </summary>
        public InvalidArgumentParserException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="InvalidArgumentParserException"/> class with a
        /// specified error message and a reference to the inner exception
        /// that caused this exception.
        /// </summary>
        public InvalidArgumentParserException(string message, Exception innerException)
            : base(message, innerException) { }

        #endregion
    }
}