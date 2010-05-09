using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    /// <summary>
    /// Represents a trivial filter for validating single values.
    /// </summary>
    /// <remarks>The filter contains no filtering condition, so that is treats
    /// every value as valid.</remarks>
    /// <typeparam name="ValueType">Specifies the type of the value object.
    /// </typeparam>
    public sealed class EmptyValueFilter<ValueType> : ValueFilter<ValueType>
    {
        #region Public methods

        /// <summary>
        /// Treats every value trivially as valid.
        /// </summary>
        /// <param name="value">The value to be checked.</param>
        /// <returns>true always</returns>
        public bool IsValid(ValueType value)
        {
            return true;
        }

        #endregion
    }
}
