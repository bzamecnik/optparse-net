using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    /// <summary>
    /// Represents an action to store a single option parameter value to the
    /// predefined destination object.
    /// </summary>
    /// <typeparam name="ValueType">Type of the parameter and the destination.
    /// </typeparam>
    /// <seealso cref="OptionValue&lt;ValueType&gt;"/>
    public sealed class StoreAction<ValueType> : StorageAction<ValueType>
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="StoreAction&lt;ValueType&gt;" /> class with the
        /// specified destination.
        /// </summary>
        /// <param name="destination">
        /// The destination where to store the option parameter value.
        /// </param>
        public StoreAction(OptionValue<ValueType> destination)
            : base(destination) { }

        #endregion

        #region Public methods

        #region Interface Action

        /// <summary>
        /// Store the option parameter into the predefined destination.
        /// </summary>
        /// <remarks>
        /// The option parameter is passed as a list. For now an option
        /// supports one or zero parameters. This can be seen from
        /// the <c>parameters.Count</c>.
        /// It is future-compatible to support multiple parameters per option.
        /// </remarks>
        /// <param name="parameters">The list of option parameters.</param>
        /// <param name="parserState">Current state of the option parser.</param>
        /// <exception cref="ParseException">
        /// any of the <c>parameters</c> is not correct</exception>
        public override void Execute(IList<string> parameters, OptionParser.State parserState)
        {
            base.Execute(parameters, parserState);
            if (parameters.Count == 1)
            {
                ValueType parsedArgument = ParseArgument(parameters[0]);
                Destination.Value = parsedArgument;
            }
            // else if (arguments.Count == 0): leave the value unset or the default value
            // assertFalse (arguments.Count > 0)
        }

        #endregion

        #endregion
    }
}
