using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dppopt
{
    /// <summary>
    /// Converts the string representation of a logical value to its boolean
    /// equivalent.
    /// </summary>
    /// <param name="argument">A string representing the logical value to
    /// convert.
    /// </param>
    /// <returns>The value of <c>argument</c> as its boolean equivalent.
    /// </returns>
    /// <exception cref="FormatException">
    /// <c>argument</c> is not in correct format
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// <c>argument</c> is null
    /// </exception>
    /// <seealso cref="Boolean.Parse"/>
    public class BoolArgumentParser : ArgumentParser<bool>
    {
        #region Public methods

        public bool ParseArgument(string argument)
        {
            bool value;
            try
            {
                value = Boolean.Parse(argument);
            }
            catch (FormatException ex)
            {
                throw new FormatException("BoolArgumentParser: Invalid argument format", ex);
            }
            return value;
        }

        #endregion
    }
}