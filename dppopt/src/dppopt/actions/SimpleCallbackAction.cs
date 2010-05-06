using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public sealed class SimpleCallbackAction : Action
    {
        #region Construction

        public SimpleCallbackAction(Callback callback)
        {
            callback_ = callback;
        }

        #endregion

        #region Public methods

        public void Execute(IList<string> parameters, OptionParser.State parserState)
        {
            // Note: the argument is ignored
            callback_(parserState);
        }

        #endregion

        #region Public delegates

        public delegate void Callback(OptionParser.State parserState);

        #endregion

        #region Private fields

        private Callback callback_;

        #endregion
    }
}
