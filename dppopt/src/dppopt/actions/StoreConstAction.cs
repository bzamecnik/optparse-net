using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public sealed class StoreConstAction<ValueType> : StorageAction<ValueType>
    {
        #region Construction

        public StoreConstAction(OptionValue<ValueType> destination, ValueType constant)
            : base(destination)
        {
            constant_ = constant;
        }

        #endregion

        #region Public methods

        public override void Execute(IList<string> parameters, OptionParser.State parserState)
        {
            Destination.Value = constant_;
        }

        #endregion

        #region Private fields

        private ValueType constant_;

        #endregion
    }
}
