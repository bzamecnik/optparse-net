using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public sealed class RangeValueFilter<ValueType> : ValueFilter<ValueType>
        where ValueType : IComparable<ValueType>
    {
        public ValueType Min {
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

        public bool IsValid(ValueType value) {
            if (minHasValue_ && (value.CompareTo(Min) < 0)) return false;
            if (maxHasValue_ && (value.CompareTo(Max) > 0)) return false;
            return true;

            // TODO: possibly better code (test it!)
            //bool lessThanMin = minHasValue_ && (value.CompareTo(Min) < 0);
            //bool moreThanMax = maxHasValue_ && (value.CompareTo(Max) > 0);
            //return lessThanMin || moreThanMax || true;
        }

        private ValueType min_;
        private bool minHasValue_;
        
        private ValueType max_;
        private bool maxHasValue_;
    }
}
