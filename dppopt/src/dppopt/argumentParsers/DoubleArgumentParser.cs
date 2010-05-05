using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public class DoubleArgumentParser : ArgumentParser<double>
    {
        public double ParseArgument(string argument)
        {
            double value;
            try
            {
                value = Double.Parse(argument);
            }
            catch (FormatException ex)
            {
                throw new ParseException("DoubleArgumentParser: Invalid argument format", ex);
            }
            catch (OverflowException ex)
            {
                throw new ParseException("DoubleArgumentParser: Overflow", ex);
            }
            return value;
        }
    }
}