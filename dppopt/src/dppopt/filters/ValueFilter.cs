using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public interface ValueFilter<ValueType>
    {
        bool IsValid(ValueType value);
    }
}
