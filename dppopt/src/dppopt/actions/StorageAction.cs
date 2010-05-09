using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    /// <summary>
    /// Represents an action to store a value into a predefined destination object.
    /// </summary>
    /// <typeparam name="ValueType">Type of the value and the destination.</typeparam>
    /// <seealso cref="OptionValue&lt;ValueType&gt;"/>
    public abstract class StorageAction<ValueType> : ParametrizedAction<ValueType>
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="StorageAction&lt;ValueType&gt;" /> class with the
        /// specified destination.
        /// </summary>
        /// <param name="destination">
        /// The destination where to store the value.
        /// </param>
        protected StorageAction(OptionValue<ValueType> destination)
        {
            Destination = destination;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the destination where to store the option value.
        /// </summary>
        public OptionValue<ValueType> Destination { get; protected set; }

        #endregion
    }
}
