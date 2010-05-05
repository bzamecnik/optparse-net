using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public sealed class EmptyValueFilter<ValueType> : ValueFilter<ValueType>
    {
        #region Public methods

        public bool IsValid(ValueType value)
        {
            return true;
        }

        #endregion
    }
}
