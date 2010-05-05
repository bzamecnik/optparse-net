using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public class IntArgumentParser : ArgumentParser<int>
    {
        #region Public methods

        public int ParseArgument(string argument)
        {
            int value;
            try
            {
                value = Int32.Parse(argument);
            }
            catch (FormatException ex)
            {
                throw new ParseException("IntArgumentParser: Invalid argument format", ex);
            }
            catch (OverflowException ex)
            {
                throw new ParseException("IntArgumentParser: Overflow", ex);
            }
            return value;
        }

        #endregion
    }
}
