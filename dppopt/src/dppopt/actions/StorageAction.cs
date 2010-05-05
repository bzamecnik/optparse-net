using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public abstract class StorageAction<ValueType> : ParametrizedAction<ValueType>
    {
        protected StorageAction(OptionValue<ValueType> destination) {
            destination_ = destination;
        }

        public OptionValue<ValueType> Destination {
            get {return destination_; }
        }

        private OptionValue<ValueType> destination_;
    }
}
