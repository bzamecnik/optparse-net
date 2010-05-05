using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public sealed class StoreAction<ValueType> : StorageAction<ValueType>
    {
        #region Construction

        public StoreAction(OptionValue<ValueType> destination)
            : base(destination) { }

        #endregion

        #region Public methods

        public override void Execute(List<string> arguments, OptionParser.State parserState)
        {
            if (arguments.Count > 0)
            {
                ValueType parsedArgument = ParseArgument(arguments[0]);
                Destination.Value = parsedArgument;
            }
        }

        #endregion
    }
}
