using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace dppopt
{
    /// <summary>
    /// Represents a definition of a command line option.
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
        /// any of the names is a not valid short or long option name
        /// or there is not at least one name
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

        /// <summary>
        /// Returns a string representation of the option.
        /// </summary>
        /// <returns>a string representing the option</returns>
        public override string ToString()
        {
            return String.Join(", ", Names.ToArray());
        }

        /// <summary>
        /// Indicates whether the specified name is a valid option name.
        /// </summary>
        /// <param name="name">The specified name.</param>
        /// <returns>true if the specified name is a valid short or long
        /// option name; false otherwise</returns>
        public static bool IsValidOptionName(string name)
        {
            return IsValidShortOptionName(name) || IsValidLongOptionName(name);
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the list of option's synonymous names (short or long).
        /// </summary>
        public IList<string> Names
        {
            // Possibly: return names_.AsReadOnly()
            // But this would disallow a scenario of intelligent handler
            // to resolve a duplicity when defining an option by removing the
            // colliding names.
            get { return names_; }
            private set { names_ = value.ToList(); }
        }

        /// <summary>
        /// Gets the action to be executed when this option is encountered
        /// among the command line arguments.
        /// </summary>
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
        /// Indicates that the option's parameter is required, ie. it does not
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
        /// Indicates that the option is required, ie. it must appear at least
        /// once in the input arugment list. The default is <c>false</c>,
        /// meaning the option is truly optional.
        /// </summary>
        public bool Required { get; set; }

        #endregion

        #region Public inner classes

        /// <summary>
        /// Represents regular expressions to match option name and parameter.
        /// </summary>
        public sealed class RegularExpressions
        {
            public static string ShortOption 
                { get { return @"-[a-zA-Z]"; } }

            public static string ShortOptionWithParam 
                { get { return "(" + ShortOption + ")(.*)"; } }

            public static string LongOption 
                { get { return @"--[a-zA-Z0-9-]*"; } }

            public static string LongOptionWithParam 
                { get { return @"(--[a-zA-Z0-9-]+)=(.*)"; } }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Returns a default name of the meta variable for the option.
        /// </summary>
        /// <remarks>
        /// The default meta variable name is taken from either the first
        /// long option name, the first short option name or the string "VAR",
        /// whichever comes first. It is then converted to upper case,
        /// the leading hyphens ("-") are stripped and other hyphens converted
        /// to underscores ("_").
        /// </remarks>
        /// <returns>The default name of the meta variable for the option.
        /// </returns>
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
            metaVar = Regex.Replace(metaVar.ToUpper(), "^--?", "");
            metaVar = Regex.Replace(metaVar, "-", "_");
            return metaVar;
        }

        /// <summary>
        /// Indicates whether the specified name is a valid short option name.
        /// </summary>
        /// <param name="name">The specified name.</param>
        /// <returns>true if the specified name is a valid short option name;
        /// false otherwise</returns>
        private static bool IsValidShortOptionName(string name)
        {
            return Regex.IsMatch(name, RegularExpressions.ShortOption);
        }

        /// <summary>
        /// Indicates whether the specified name is a valid long option name.
        /// </summary>
        /// <param name="name">The specified name.</param>
        /// <returns>true if the specified name is a valid long option name;
        /// false otherwise</returns>
        private static bool IsValidLongOptionName(string name)
        {
            return Regex.IsMatch(name, RegularExpressions.LongOption);
        }

        #endregion

        #region Private fields
        
        /// <summary>
        /// Represents the list of option names.
        /// </summary>
        /// <see cref="Names"/>
        private List<string> names_;

        /// <summary>
        /// Represents the meta variable for the option.
        /// </summary>
        /// <see cref="MetaVariable"/>
        private string metaVariable_ = null;

        /// <summary>
        /// Represents the number of parameters the option accepts.
        /// </summary>
        /// <see cref="ParametersCount"/>
        private int parametersCount_ = 1;
        
        #endregion
    }
}
