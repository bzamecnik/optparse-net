using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    /// <summary>
    /// Represents an action which calls a callback function specified in the
    /// form of a delegate.
    /// </summary>
    /// <remarks>
    /// In contrast with <see cref="SimpleCallbackAction"/> this action might
    /// use the option parameters. Note that the callback function might
    /// interact with the <see cref="OptionParser.State"/>. 
    /// </remarks>
    public sealed class CallbackAction<ValueType> : ParametrizedAction<ValueType>
    {
        #region Construction

        // <summary>
        /// Initializes a new instance of <see cref="CallbackAction"/>
        /// class with the specified callback function.
        /// </summary>
        /// <param name="callback">The callback function to be called.</param>
        public CallbackAction(Callback callback)
        {
            callback_ = callback;
        }

        #endregion

        #region Public methods

        #region Interface Action

        /// <summary>
        /// Executes the callback action cosidering the option parameters.
        /// </summary>
        /// <param name="parameters">The list of option parameters.</param>
        /// <param name="parserState">Current state of the option parser.
        /// </param>
        public override void Execute(IList<string> parameters, OptionParser.State parserState)
        {
            callback_(parameters, parserState);
        }

        #endregion

        #endregion

        #region Public delegates

        /// <summary>
        /// Represents the callback function to be called by the action.
        /// </summary>
        /// <param name="parameters">The list of option parameters.</param>
        /// <param name="parserState">Current state of parsing.</param>
        public delegate void Callback(IList<string> parameters, OptionParser.State parserState);

        #endregion

        #region Private fields

        /// <summary>
        /// The callback function.
        /// </summary>
        private Callback callback_;

        #endregion
    }
}
