using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    /// <summary>
    /// Action to store a value to a predefined destination object
    /// (<see cref="OptionValue"/>).
    /// </summary>
    /// <typeparam name="ValueType">Type of the value and the destination.</typeparam>
    public abstract class StorageAction<ValueType> : ParametrizedAction<ValueType>
    {
        #region Construction

        /// <summary>
        /// Create the action and set the destination.
        /// </summary>
        /// <param name="destination"></param>
        protected StorageAction(OptionValue<ValueType> destination)
        {
            Destination = destination;
        }

        #endregion

        #region Public properties

        public OptionValue<ValueType> Destination { get; protected set; }

        #endregion
    }
}
