using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    /// <summary>
    /// Represents a storage for a value obtained from option's parameter.
    /// </summary>
    /// <remarks>
    /// When options are processed they may store value of their parameter
    /// to <c>OptionValue</c>. It is strongly typed and must correspond to
    /// the type of the option's parameter. If the associated option did not
    /// occured in the input argument list (and thus could not stor any value
    /// here) and its parameter was not defined as required a default value
    /// (either implicit or user defined) remains in the
    /// <see cref="OptionValue" />.
    /// </remarks>
    /// <typeparam name="ValueType">Specifies the type of the value.
    /// </typeparam>
    public class OptionValue<ValueType>
    {
        #region Construction

        /// <summary>
        /// Creates a new option value instance with an implicit default value.
        /// </summary>
        /// <remarks>If <c>ValueType</c> is a value type <c>Value</c>is set
        /// to its implicit default value. In case it is a reference type
        /// <c>Value</c>is set to <c>null</c>.</remarks>
        public OptionValue() { }

        /// <summary>
        /// Creates a new option value instance and initializes it with given
        /// default value.
        /// </summary>
        public OptionValue(ValueType defaultValue) { Value = defaultValue; }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the stored option value.
        /// </summary>
        public ValueType Value
        {
            get;
            set;
        }

        #endregion
    }
}
