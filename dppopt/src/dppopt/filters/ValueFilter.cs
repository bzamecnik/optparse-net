using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    // TODO: rename: ValueFilter -> IValueFilter

    /// <summary>
    /// Represents a filter for validating single values.
    /// </summary>
    /// <typeparam name="ValueType">
    /// Specifies the type of the value object.
    /// </typeparam>
    public interface ValueFilter<ValueType>
    {
        /// <summary>
        /// Checks whether the value conforms to conditions specified in the
        /// filter.
        /// </summary>
        /// <param name="value">The value to be checked.</param>
        /// <returns>true if the value is valid according to the filter;
        /// otherwise, false</returns>
        bool IsValid(ValueType value);
    }
}
