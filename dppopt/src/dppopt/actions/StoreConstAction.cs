using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public sealed class StoreConstAction<ValueType> : StorageAction<ValueType>
    {
        public StoreConstAction(OptionValue<ValueType> destination, ValueType constant)
            : base(destination)
        {
            constant_ = constant;
        }

        public override void Execute(List<string> arguments, OptionParser parser)
        {
            Destination.Value = constant_;
        }

        private ValueType constant_;
    }
}
