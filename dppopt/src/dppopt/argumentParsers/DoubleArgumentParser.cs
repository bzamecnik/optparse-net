using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    /// <summary>
    /// Represents a parser which converts the string representation of a
    /// number to its double-precision floating-point equivalent.
    /// </summary>
    public class DoubleArgumentParser : ArgumentParser<double>
    {
        #region Public methods

        #region Interface ArgumentParser

        /// <summary>
        /// Converts the string representation of a number to its
        /// double-precision floating-point equivalent.
        /// </summary>
        /// <param name="argument">A string representing the number to convert.
        /// </param>
        /// <returns>The value of <c>argument</c> as a double-precision
        /// floating-point number.
        /// </returns>
        /// <exception cref="FormatException">
        /// <c>argument</c> is not in correct format
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <c>argument</c> is null
        /// </exception>
        /// <seealso cref="Double.Parse(string)"/>
        public double ParseArgument(string argument)
        {
            double value;
            try
            {
                value = Double.Parse(argument);
            }
            catch (FormatException ex)
            {
                throw new FormatException("DoubleArgumentParser: Invalid argument format: " + argument, ex);
            }
            catch (OverflowException ex)
            {
                throw new FormatException("DoubleArgumentParser: Overflow: " + argument, ex);
            }
            return value;
        }

        #endregion

        #endregion
    }
}