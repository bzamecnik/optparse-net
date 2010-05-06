using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace dppopt
{
    /// <summary>
    /// Definition of an option.
    /// </summary>
    public sealed class Option
    {
        #region Construction

        public Option(string[] names, string helpText, Action action)
        {
            if (!AreValidOptionNames(names))
            {
                throw new ArgumentException("Invalid option names:" + String.Join(", ", names));
            }
            Action = action;
            HelpText = helpText;
            Names = names.ToList();
            ParametersCount = 1;
        }

        #endregion

        #region Public methods

        public void HandleOption(List<string> arguments, OptionParser.State parserState)
        {
            Action.Execute(arguments, parserState);
        }

        #endregion

        #region Public properties

        public List<string> Names
        {
            get
            {
                // TODO: return a copy or unmodifiable variant
                return names_;
            }
            private set { names_ = value; }
        }

        public Action Action { get; private set; }

        public bool Required { get; set; } // TODO: default: false

        public bool ParametersRequired { get; set; } // TODO: default: false

        public int ParametersCount { get; set; } // TODO: [0;1], default: 1

        public string HelpText { get; private set; }

        public string MetaVariable
        {
            get
            {
                if (metaVariable_ == null)
                {
                    metaVariable_ = GetDefaultMetaVariableName();
                }
                return metaVariable_;
            }
            set
            {
                metaVariable_ = value;
            }
        }

        #endregion

        #region Private methods

        private string GetDefaultMetaVariableName()
        {
            // TODO: compute from Names
            // - get the first short or long option name
            // - change it somehow:
            //   - example: --my-version -> MY_VERSION
            return "";
        }

        private static bool IsValidShortOptionName(string name) {
            return Regex.IsMatch(name, @"-[a-zA-Z]");
        }

        private static bool IsValidLongOptionName(string name)
        {
            return Regex.IsMatch(name, @"--[a-zA-Z]+");
        }

        private static bool AreValidOptionNames(string[] names)
        {
            return names.All(name => IsValidShortOptionName(name) || IsValidShortOptionName(name));
        }

        #endregion

        #region Private fields
        
        private List<string> names_;
        private string metaVariable_ = null;
        
        #endregion
    }
}
