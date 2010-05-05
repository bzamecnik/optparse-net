using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    class Options
    {
        public void AddOption(Option option)
        {
            foreach (string name in option.Names)
            {
                optionMap_.Add(name, option);
            }
        }

        public Option GetOption(string name)
        {
            return optionMap_[name];
        }

        public bool HasOption(string name)
        {
            return optionMap_.ContainsKey(name);
        }

        private Dictionary<string, Option> optionMap_ = new Dictionary<string, Option>();
    }
}
