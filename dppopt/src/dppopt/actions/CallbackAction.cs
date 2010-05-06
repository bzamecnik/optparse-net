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

        public override void Execute(IList<string> parameters, OptionParser.State parserState)
        {
            callback_(parameters, parserState);
        }

        public delegate void Callback(IList<string> parameters, OptionParser.State parserState);

        Callback callback_;
    }
}
