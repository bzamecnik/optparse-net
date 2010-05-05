using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public class OptionValue<ValueType>
    {
        #region Construction

        public OptionValue() { }

        public OptionValue(ValueType defaultValue) { Value = defaultValue; }

        #endregion

        #region Public properties

        public ValueType Value
        {
            get;
            set;
        }

        #endregion
    }
}
