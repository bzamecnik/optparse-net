using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    /// <summary>
    /// Represents a trivial parser.
    /// </summary>
    public class StringArgumentParser : ArgumentParser<string>
    {
        #region Public methods

        #region Interface ArgumentParser

        /// <summary>
        /// Trivially passes the string without any modification.
        /// </summary>
        /// <param name="argument">A string to convert.</param>
        /// <returns>The <c>argument</c> unmodified.</returns>
        public string ParseArgument(string argument)
        {
            return argument;
        }

        #endregion

        #endregion
    }
}