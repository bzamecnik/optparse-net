using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public class OptionParser
    {
        public void AddOption(Option option) {
            options_.AddOption(option);
        }

        public List<string> ParseArguments(string[] arguments) {
            List<string> remainingArguments = new List<string>(arguments);
            // do parsing stuff ...

            return remainingArguments;
        }

        private Options options_ = new Options();
    }
}
