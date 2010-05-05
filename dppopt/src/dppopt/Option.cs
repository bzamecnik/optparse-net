using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public class Option
    {
        public Option(string[] names, string helpText, Action action)
        {
            action_ = action;
            helpText_ = helpText;
            names_ = names.ToList();
        }

        public List<string> Names
        {
            get {
                // TODO: return a copy or unmodifiable variant
                return names_;
            }
        }

        public Action Action
        {
            get {return action_; }
        }

        public bool Required { get; set; } // TODO: default: false
        
        public bool ParametersRequired { get; set; } // TODO: default: false

        public int ParametersCount { get; set; } // TODO: [0;1], default: 1

        public string HelpText
        {
            get { return helpText_; }
        }

        public string MetaVariable {
            get {
                if (metaVariable_ == null) {
                    metaVariable_ = GetDefaultMetaVariable();
                }
                return metaVariable_;
            }
            set {
                metaVariable_ = value;
            }
        }

        private string GetDefaultMetaVariable() {
            // TODO: compute from Names
            // - get the first short or long option name
            // - change it somehow:
            //   - example: --my-version -> MY_VERSION
            return "";
        }

        public void HandleOption(List<string> arguments, OptionParser parser) {
            Action.Execute(arguments, parser);
        }

        Action action_;
        List<string> names_;
        string helpText_;
        string metaVariable_ = null;
    }
}
