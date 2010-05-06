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

        public ChoiceValueFilter(ValueType[] choices)
        {
            choices_ = new HashSet<ValueType>(choices);
        }

        #endregion

        #region Public methods

        public bool IsValid(ValueType value)
        {
            return choices_.Contains(value);
        }

        #endregion

        #region Private fields

        private HashSet<ValueType> choices_;

        #endregion
    }
}
