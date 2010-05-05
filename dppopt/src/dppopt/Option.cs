using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public class Option
    {
        public Option(Action action) {
            action_ = action;
        }

        public List<string> Names
        {
            get;
            set;
        }

        public Action Action
        {
            get {return action_; }
        }

        public string HelpText
        {
            get;
            set;
        }

        public void HandleOption(string argument, OptionParser parser) {
            Action.Execute((new string[] { argument }).ToList<string>(), parser);
        }

        Action action_;
    }
}
