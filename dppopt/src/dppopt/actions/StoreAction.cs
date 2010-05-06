using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    /// <summary>
    /// Action to store the option parameter value to the predefined
    /// destination object (<see cref="OptionValue"/>).
    /// </summary>
    /// <typeparam name="ValueType">Type of the parameter and the destination.</typeparam>
    public sealed class StoreAction<ValueType> : StorageAction<ValueType>
    {
        #region Construction

        public StoreAction(OptionValue<ValueType> destination)
            : base(destination) { }

        #endregion

        #region Public methods

        public override void Execute(IList<string> parameters, OptionParser.State parserState)
        {
            if (parameters.Count == 1)
            {
                ValueType parsedArgument = ParseArgument(parameters[0]);
                Destination.Value = parsedArgument;
            }
            // else if (arguments.Count == 0): leave the value unset or the default value
            // assertFalse (arguments.Count > 0)
        }

        #endregion
    }
}
