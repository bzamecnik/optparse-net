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
                optionsMap_.Add(name, option);
            }
        }

        public Option GetOption(string name)
        {
            return optionsMap_[name];
        }

        public bool HasOption(string name)
        {
            return optionsMap_.ContainsKey(name);
        }

        public List<Option> ToList() {
            return optionsMap_.Values.ToList();
        }

        private Dictionary<string, Option> optionsMap_ = new Dictionary<string, Option>();
    }
}
