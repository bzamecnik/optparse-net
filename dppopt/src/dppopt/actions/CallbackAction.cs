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

        public override void Execute(List<string> arguments, OptionParser.State parserState)
        {
            callback_(arguments, parserState);
        }

        public delegate void Callback(List<string> arguments, OptionParser.State parserState);

        Callback callback_;
    }
}
