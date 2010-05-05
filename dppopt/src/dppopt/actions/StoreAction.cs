using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public sealed class StoreAction<ValueType> : StorageAction<ValueType>
    {
        public StoreAction(OptionValue<ValueType> destination)
            : base(destination) { }

        public override void Execute(List<string> arguments, OptionParser parser)
        {
            if (arguments.Count > 0)
            {
                ValueType parsedArgument = ParseArgument(arguments[0]);
                Destination.Value = parsedArgument;
            }
        }
    }
}
