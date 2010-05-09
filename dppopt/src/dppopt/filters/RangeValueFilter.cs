using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    // TODO: rename: range -> interval

    /// <summary>
    /// Represents a filter where the valid values are inside a specified
    /// inclusive range.
    /// </summary>
    /// <typeparam name="ValueType">
    /// Specifies the type of the value object. It must be comparable.
    /// </typeparam>
    public sealed class RangeValueFilter<ValueType> : ValueFilter<ValueType>
        where ValueType : IComparable<ValueType>
    {
        #region Public methods

        /// <summary>
        /// Checks whether the value is in the specified inclusive range.
        /// </summary>
        /// <remarks>
        /// The endpoints of the range are specified by the <see cref="Min"/>
        /// and <see cref="Max"/> properties. Each endpoint is checked only if
        /// its value has been set. An unset endpoint means an infinite
        /// endpoint which results in a half-bounded range. By default, both
        /// endpoints are unset resuling in an unbounded range. The value of
        /// each endpoint is included in the valid range. If the
        /// <see cref="Min"/> is greated than <see cref="Max"/> the range
        /// is treated as empty.
        /// </remarks>
        /// <param name="value">The value to be checked.</param>
        /// <returns>true if the value is inside the range (either bounded,
        /// half-bounded or unbounded); false otherwise (if the value is
        /// outside the range or the range is empty)</returns>
        public bool IsValid(ValueType value)
        {
            if (minHasValue_ && (value.CompareTo(Min) < 0)) return false;
            if (maxHasValue_ && (value.CompareTo(Max) > 0)) return false;
            return true;

            // TODO: possibly a better code (test it!)
            //bool lessThanMin = minHasValue_ && (value.CompareTo(Min) < 0);
            //bool moreThanMax = maxHasValue_ && (value.CompareTo(Max) > 0);
            //return lessThanMin || moreThanMax || true;
        }

        #endregion

        #region Public properties
        
        /// <summary>
        /// Represents the lower endpoint of the left-closed range, ie. its
        /// inclusive minimum. By default the value is treated as not set and
        /// the range as left-unbounded.
        /// </summary>
        public ValueType Min
        {
            get { return min_; }
            set
            {
                min_ = value;
                minHasValue_ = true;
            }
        }

        /// <summary>
        /// Represents the higher endpoint of the right-closed range, ie. its
        /// inclusive maximum. By default the value is treated as not set and
        /// the range as right-unbounded.
        /// </summary>
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
