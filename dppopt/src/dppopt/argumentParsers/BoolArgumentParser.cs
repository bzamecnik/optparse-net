using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dppopt
{
    public class BoolArgumentParser : ArgumentParser<bool>
    {
        public bool ParseArgument(string argument)
        {
            bool value;
            try
            {
                value = Boolean.Parse(argument);
            }
            catch (FormatException ex)
            {
                throw new ParseException("BoolArgumentParser: Invalid argument format", ex);
            }
            return value;
        }
    }
}