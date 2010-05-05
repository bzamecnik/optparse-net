using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public sealed class SimpleCallbackAction : Action
    {
        public SimpleCallbackAction(Callback callback) {
            callback_ = callback;
        }

        public void Execute(List<string> arguments, OptionParser parser) {
            // ignore arguments
            callback_(parser);
        }

        public delegate void Callback(OptionParser parser);

        Callback callback_;
    }
}
