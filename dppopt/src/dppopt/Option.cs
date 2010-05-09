using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace dppopt
{
    /// <summary>
    /// Definition of an command line option.
    /// </summary>
    /// <remarks>
    /// <para>
    /// An option is a special command line argument. When encountered in the
    /// list of command line arguments an action associated with the option
    /// is triggered.
    /// </para>
    /// <para>The option can have several synonymous names - short or
    /// long. Short names are in form -[a-zA-Z], that is a hyphen followed by
    /// a single letter. Long names are in form --[a-zA-Z0-9-]*, that is two
    /// hyphen followed by any number of letter, number or hyphens.
    /// The names are case-sensitive.
    /// </para>
    /// <para>
    /// The option can have an associated parameter, which can be either the
    /// following argument or directly appended after the option. For short
    /// option there is no separator (eg. "-freadme.txt"), for long options
    /// the separator is the "=" sign (eg. "--file=readme.txt").</para>
    /// </remarks>
    public sealed class Option
    {
        #region Construction

        /// <summary>
        /// Create a new option instance with specified names, help text and action.
        /// </summary>
        /// <remarks>
        /// Option names, help text and action are then accessible using
        /// <see cref="Names" />, <see cref="HelpText" /> and
        /// <see cref="Action" /> properties.
        /// </remarks>
        /// <param name="names">synonymous short (eg. "-f") and/or long (eg.
        /// "--file-name") names, at least one</param>
        /// <param name="helpText">help text to describe the option</param>
        /// <param name="action">action to be executed when the option is
        /// encountered among the command line arguments</param>
        /// <exception cref="ArgumentException">
        /// Throws if any of the names is a not valid short or long option name
        /// or if there is not at least one name.
        /// </exception>
        public Option(string[] names, string helpText, Action action)
        {
            if (!(names.Length >= 1) && (names.All(name =>
                    IsValidOptionName(name)))
                )
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

        /// <summary>
        /// Description of the option which can appear in the automatically
        /// generated program usage help.
        /// </summary>
        /// <remarks>
        /// The text may contain a meta variable name to represent the option
        /// parameter.
        /// </remarks>
        /// <see cref="MetaVariable" />
        public string HelpText { get; private set; }

        /// <summary>
        /// Meta variable represents the parameter in the option's help text.
        /// </summary>
        /// <remarks>
        /// By default it is generated from the first long option name or the
        /// first short option name. But sometimes it might be useful to
        /// set it by hand, when the generated default value is not suitable.
        /// </remarks>
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

        /// <summary>
        /// States that the option's parameter is required, ie. it does not
        /// accept a default value in case the parameter is not given.
        /// </summary>
        /// <remarks>
        /// The property name is in plural to assure future
        /// compatibility if multiple parameters per option will be supported.
        /// </remarks>
        public bool ParametersRequired { get; set; }

        /// <summary>
        /// The number of parameters the option accepts. Allowed range is
        /// [0; 1], default is 1.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the parameter is required this is the exact number of
        /// parameters. Else the option can also accept zero parameters,
        /// which results in a default value.
        /// </para>
        /// <para>
        /// Although currently an option can have at most one parameter, this
        /// property is dsigned to assure future compatibility in case
        /// multiple parameters per option will be supported.
        /// </para>
        /// </remarks>
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

        /// <summary>
        /// States that the option is required, ie. it must appear at least
        /// once in the input arugment list. The default is <c>false</c>,
        /// meaning the option is truly optional.
        /// </summary>
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
            return Regex.IsMatch(name, @"--[a-zA-Z0-9-]*");
        }

        public static bool IsValidOptionName(string name)
        {
            return (IsValidShortOptionName(name) ||
                    IsValidLongOptionName(name)
                );
        }

        #endregion

        #region Private fields
        
        private List<string> names_;
        private string metaVariable_ = null;
        int parametersCount_ = 1;
        
        #endregion
    }
}
