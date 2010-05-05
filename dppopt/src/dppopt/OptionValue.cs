using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public class OptionValue<ValueType>
    {
        public OptionValue() { }

        public OptionValue(ValueType defaultValue) { value_ = defaultValue; }

        public ValueType Value {
            get { return value_; }
            set { value_ = value; }
        }

        ValueType value_;
    }
}
