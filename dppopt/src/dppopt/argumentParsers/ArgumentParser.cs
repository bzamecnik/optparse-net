using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public interface ArgumentParser<ValueType>
    {
        ValueType ParseArgument(string argument);
    }
}
