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
    /// <remarks>Note that the callback function might interact with the
    /// <see cref="OptionParser.State"/>. However, it ignores any option
    /// parameters.</remarks>
    public sealed class SimpleCallbackAction : Action
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of <see cref="SimpleCallbackAction"/>
        /// class with the specified callback function.
        /// </summary>
        /// <param name="callback">The callback function to be called.</param>
        public SimpleCallbackAction(Callback callback)
        {
            callback_ = callback;
        }

        #endregion

        #region Public methods

        #region Interface Action

        /// <summary>
        /// Executes the callback action ignoring any option parameters.
        /// </summary>
        /// <param name="parameters">The list of option parameters which is
        /// ignored, however.</param>
        /// <param name="parserState">Current state of the option parser.
        /// </param>
        public void Execute(IList<string> parameters, OptionParser.State parserState)
        {
            callback_(parserState);
        }

        #endregion

        #endregion

        #region Public delegates

        /// <summary>
        /// Represents the callback function to be called by the action.
        /// </summary>
        /// <param name="parserState">Current state of parsing.</param>
        public delegate void Callback(OptionParser.State parserState);

        #endregion

        #region Private fields

        /// <summary>
        /// The callback function.
        /// </summary>
        private Callback callback_;

        #endregion
    }
}
