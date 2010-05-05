using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public sealed class ChoiceValueFilter<ValueType> : ValueFilter<ValueType>
        where ValueType : IEquatable<ValueType>
    {
        #region Construction

        public ChoiceValueFilter(ISet<ValueType> choices)
        {
            choices_ = choices;
        }

        public ChoiceValueFilter(ValueType[] choices)
            : this(new HashSet<ValueType>(choices)) { }

        #endregion

        #region Public methods

        public bool IsValid(ValueType value)
        {
            return choices_.Contains(value);
        }

        #endregion

        #region Private fields

        private ISet<ValueType> choices_;

        #endregion
    }
}
