using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public interface Action
    {
        void Execute(List<string> arguments, OptionParser parser);
    }
}
