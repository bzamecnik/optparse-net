using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public sealed class ChoiceValueFilter<ValueType> : ValueFilter<ValueType>
        where ValueType : IEquatable<ValueType>
    {
        public ChoiceValueFilter(ISet<ValueType> choices) {
            choices_ = choices;
        }

        public ChoiceValueFilter(ValueType[] choices)
            : this(new HashSet<ValueType>(choices)) { }

        public bool IsValid(ValueType value) {
            return choices_.Contains(value);
        }

        private ISet<ValueType> choices_;
    }
}
