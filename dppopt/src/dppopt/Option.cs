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
            Names = names;
            ParametersCount = 1;
            ParametersRequired = false;
            Required = false;
        }

        #endregion

        #region Public methods

        public void HandleOption(IList<string> parameters, OptionParser.State parserState)
        {
            Action.Execute(parameters, parserState);
        }

        #endregion

        #region Public properties

        public IList<string> Names
        {
            // Possibly: return names_.AsReadOnly()
            // But this would disallow a scenario of intelligent handler
            // to resolve a duplicity when defining an option by removing the
            // colliding names.
            get { return names_; }
            private set { names_ = value.ToList(); }
        }

        public Action Action { get; private set; }

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

        public bool ParametersRequired { get; set; }

        public int ParametersCount
        {
            get { return parametersCount_; }
            set {
                if ((value < 0) || (value > 1)) {
                    throw new ArgumentException("Invalid number of parameters (allowed range: [0; 1])");
                }
                parametersCount_ = value;
            }
        }

        public bool Required { get; set; }

        #endregion

        #region Private methods

        private string GetDefaultMetaVariableName()
        {
            string metaVar = Names.FirstOrDefault(name => IsValidLongOptionName(name));
            if (metaVar.Length == 0) {
                metaVar = Names.FirstOrDefault(name => IsValidShortOptionName(name));
            }
            if (metaVar.Length == 0)
            {
                metaVar = "VAR";
            }
            metaVar = Regex.Replace(metaVar.ToUpper(), @"^--?", "");
            metaVar = Regex.Replace(metaVar, @"-", "_");
            return metaVar;
        }

        private static bool IsValidShortOptionName(string name) {
            return Regex.IsMatch(name, @"-[a-zA-Z]");
        }

        private static bool IsValidLongOptionName(string name)
        {
            return Regex.IsMatch(name, @"--[a-zA-Z]*");
        }

        private static bool AreValidOptionNames(string[] names)
        {
            return names.All(name => IsValidShortOptionName(name) || IsValidShortOptionName(name));
        }

        #endregion

        #region Private fields
        
        private List<string> names_;
        private string metaVariable_ = null;
        int parametersCount_ = 1;
        
        #endregion
    }
}
