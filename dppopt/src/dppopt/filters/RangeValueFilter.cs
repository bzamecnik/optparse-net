using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public sealed class RangeValueFilter<ValueType> : ValueFilter<ValueType>
        where ValueType : IComparable<ValueType>
    {
        #region Public methods

        public bool IsValid(ValueType value)
        {
            if (minHasValue_ && (value.CompareTo(Min) < 0)) return false;
            if (maxHasValue_ && (value.CompareTo(Max) > 0)) return false;
            return true;

            // TODO: possibly better code (test it!)
            //bool lessThanMin = minHasValue_ && (value.CompareTo(Min) < 0);
            //bool moreThanMax = maxHasValue_ && (value.CompareTo(Max) > 0);
            //return lessThanMin || moreThanMax || true;
        }

        #endregion

        #region Public properties

        public ValueType Min
        {
            get { return min_; }
            set
            {
                min_ = value;
                minHasValue_ = true;
            }
        }
        public ValueType Max
        {
            get { return max_; }
            set
            {
                max_ = value;
                maxHasValue_ = true;
            }
        }

        #endregion

        #region Private fields

        private ValueType min_;
        private bool minHasValue_;

        private ValueType max_;
        private bool maxHasValue_;

        #endregion
    }
}
