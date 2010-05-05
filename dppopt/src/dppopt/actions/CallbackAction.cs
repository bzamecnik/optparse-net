using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public sealed class CallbackAction<ValueType> : ParametrizedAction<ValueType>
    {
        public CallbackAction(Callback callback)
        {
            callback_ = callback;
        }

        public override void Execute(List<string> arguments, OptionParser parser) {
            callback_(arguments, parser);
        }

        public delegate void Callback(List<string> arguments, OptionParser parser);

        Callback callback_;
    }
}
