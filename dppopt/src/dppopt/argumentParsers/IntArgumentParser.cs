using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    // TODO: rename: IntArgumentParser -> Int32ArgumentParser

    /// <summary>
    /// Represents a parser which converts the string representation of a
    /// number to its 32-bit signed integer equivalent.
    /// </summary>
    public class IntArgumentParser : ArgumentParser<int>
    {
        #region Public methods

        /// <summary>
        /// Converts the string representation of a number to its 32-bit
        /// signed integer equivalent.
        /// </summary>
        /// <param name="argument">A string representing the number to convert.
        /// </param>
        /// <returns>The value of <c>argument</c> as a 32-bit signed integer.
        /// </returns>
        /// <exception cref="FormatException">
        /// <c>argument</c> is not in correct format
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <c>argument</c> is null
        /// </exception>
        /// <seealso cref="Int32.Parse"/>
        public int ParseArgument(string argument)
        {
            int value;
            try
            {
                value = Int32.Parse(argument);
            }
            catch (FormatException ex)
            {
                throw new FormatException("IntArgumentParser: Invalid argument format: " + argument, ex);
            }
            catch (OverflowException ex)
            {
                throw new FormatException("IntArgumentParser: Overflow: " + argument, ex);
            }
            return value;
        }

        #endregion
    }
}
