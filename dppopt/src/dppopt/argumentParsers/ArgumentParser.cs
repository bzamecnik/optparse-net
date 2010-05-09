using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    /// <summary>
    /// Represents a parser which converts a string to a value of specified
    /// type.
    /// </summary>
    /// <remarks>
    /// Its purpose is to parse a single command line argument treated as an
    /// command line option parameter into the desired type.
    /// </remarks>
    /// <typeparam name="ValueType">
    /// Specifies the type of the value.
    /// </typeparam>
    public interface ArgumentParser<ValueType>
    {
        /// <summary>
        /// Converts the string representation into its value in the specified type.
        /// </summary>
        /// <param name="argument">A string to convert.</param>
        /// <returns>An instance of <see cref="ValueType"/> equivalent to its
        /// string representaion in <c>argument</c>.</returns>
        /// <exception cref="FormatException">
        /// <c>argument</c> is not in correct format
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <c>argument</c> is null
        /// </exception>
        ValueType ParseArgument(string argument);
    }
}
