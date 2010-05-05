using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public sealed class RangeValueFilter<ValueType> : ValueFilter<ValueType>
        where ValueType : IComparable<ValueType>
    {
        public ValueType? Min { get; set; }
        public ValueType? Max { get; set; }

        public bool IsValid(ValueType value) {
            if (Min.HasValue && (value.CompareTo(Min.Value) < 0)) return false;
            if (Max.HasValue && (value.CompareTo(Max.Value) > 0)) return false;
            return true;

            // TODO: possibly better code (test it!)
            //bool lessThanMin = Min.HasValue && (value.CompareTo(Min.Value) < 0);
            //bool moreThanMax = Max.HasValue && (value.CompareTo(Max.Value) > 0);
            //return lessThanMin || moreThanMax || true;
        }
    }
}
