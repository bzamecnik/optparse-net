using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    /// <summary>
    /// Represents an action to store a constant value to the
    /// predefined destination object.
    /// </summary>
    /// <typeparam name="ValueType">Type of the parameter and the destination.
    /// </typeparam>
    /// <seealso cref="OptionValue&lt;ValueType&gt;"/>
    public sealed class StoreConstAction<ValueType> : StorageAction<ValueType>
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="StoreAction&lt;ValueType&gt;" /> class with the
        /// specified destination and constant value.
        /// </summary>
        /// <param name="destination">
        /// The destination where to store the option parameter value.
        /// </param>
        public StoreConstAction(OptionValue<ValueType> destination, ValueType constant)
            : base(destination)
        {
            constant_ = constant;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Store a constant into the predefined destination regardsless of the
        /// option parameters.
        /// </summary>
        /// <param name="parameters">The list of option parameters. It is ignored.</param>
        /// <param name="parserState">Current state of the option parser.</param>
        public override void Execute(IList<string> parameters, OptionParser.State parserState)
        {
            Destination.Value = constant_;
        }

        #endregion

        #region Private fields

        /// <summary>
        /// The constant to be stored into the destination.
        /// </summary>
        private ValueType constant_;

        #endregion
    }
}
