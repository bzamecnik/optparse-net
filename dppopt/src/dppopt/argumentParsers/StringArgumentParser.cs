using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public class StringArgumentParser : ArgumentParser<string>
    {
        public string ParseArgument(string argument)
        {
            return argument;
        }
    }
}