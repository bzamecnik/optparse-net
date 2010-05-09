using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    /// <summary>
    /// Represents a filter where the valid values are elements of a
    /// specified set.
    /// </summary>
    /// <typeparam name="ValueType">
    /// Specifies the type of the value object. It must be equatable.
    /// </typeparam>
    public sealed class ChoiceValueFilter<ValueType> : ValueFilter<ValueType>
        where ValueType : IEquatable<ValueType>
    {
        #region Construction

        /// <summary>
        /// Creates a new instance of the filter and initializes it with a
        /// set of valid values.
        /// </summary>
        /// <param name="choices">The collection of valid values whose
        /// elements are copied. Duplicate elements are allowed but treated
        /// as single elements.</param>
        /// <exception cref="ArgumentNullException">choices is null</exception>
        public ChoiceValueFilter(IEnumerable<ValueType> choices)
        {
            choices_ = new HashSet<ValueType>(choices);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Checks whether the value is an element of the set of allowed values.
        /// </summary>
        /// <param name="value">The value to be checked.</param>
        /// <returns>true if the value is valid according to the filter;
        /// otherwise, false</returns>
        public bool IsValid(ValueType value)
        {
            return choices_.Contains(value);
        }

        #endregion

        #region Private fields

        /// <summary>
        /// Represents the set of allowed values.
        /// </summary>
        private HashSet<ValueType> choices_;

        #endregion
    }
}
