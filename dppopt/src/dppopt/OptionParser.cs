using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace dppopt
{
    // TODO: a better name could be CommandLineParser

    /// <summary>
    /// The command line option parser processes the list of command line arguments
    /// taking actions for encountered predefined options.
    /// The rest of arguments is returned back.
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    public class OptionParser
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of <see cref="OptionParser"/> class.
        /// </summary>
        /// <remarks>
        /// Some default options are defined: help, version and "--" for
        /// termination the list of options.
        /// </remarks>
        public OptionParser()
        {
            helpFormatter_ = new SimpleHelpFormatter();
            programInfo_ = new ProgramInformation(this);
            UseDefaultHelpOption = true;
            UseDefaultVersionOption = true;
            ProgramExitEnabled = true;
            AddDefaultOptions();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Defines the specified option.
        /// </summary>
        /// <param name="option">The option to be defined.</param>
        /// <exception cref="ArgumentException">
        /// an option has been already defined with the same name as one of
        /// the names of this option 
        /// </exception>
        public void AddOption(Option option)
        {
            options_.AddOption(option);
        }

        /// <summary>
        /// Parses the list of command line arguments.
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public IList<string> ParseArguments(string[] arguments)
        {
            List<string> remainingArguments = new List<string>(arguments);
            State parserState = new State(this);

            HashSet<Option> requiredOptions = new HashSet<Option>
                (options_.ToList().FindAll(option => option.Required));

            // while (there are any remaining input arguments) 
            // and (parsing should not be stopped)
            while (parserState.ContinueParsing && remainingArguments.Count() > 0)
            {
                string name = remainingArguments.First();
                string parameter = "";
                bool parameterAlreadySet = false;

                string patternLong = Option.RegularExpressions.LongOptionWithParam;
                string patternShort = Option.RegularExpressions.ShortOptionWithParam;
                // handle parameters like:
                //   --number=42
                if (Regex.IsMatch(name, patternLong))
                {
                    Match match = Regex.Match(name, patternLong);
                    name = match.Groups[1].Value;
                    parameter = match.Groups[2].Value;
                    parameterAlreadySet = true;
                }
                // handle parameters like:
                //   -n42
                else if (Regex.IsMatch(name, patternShort))
                {
                    Match match = Regex.Match(name, patternShort);
                    name = match.Groups[1].Value;
                    parameter = match.Groups[2].Value;
                    parameterAlreadySet = true;
                }

                // if the next input argument starts like an option
                if (Option.IsValidOptionName(name))
                {
                    // fetch the argument
                    if (!options_.HasOption(name))
                    {
                        // error if the option is unknown or somehow bad
                        throw new ParseException("Unknown option name:" + name);
                    }
                    // identify the option
                    Option option;
                    option = options_.GetOption(name);

                    if (option.Required)
                    {
                        requiredOptions.Remove(option);
                    }

                    // delete the option from the remaining input arguments list
                    remainingArguments.RemoveAt(0);


                    // if the option should take some parameters 
                    // and there are any in the input list
                    List<string> parameters = new List<string>();

                    if (!parameterAlreadySet && option.ParametersCount > 0 
                        && remainingArguments.Count() > 0)
                    {
                        // fetch one argument and delete it from the list
                        parameter = remainingArguments.First();
                        parameterAlreadySet = true;

                        if (!options_.HasOption(parameter))
                        {
                            remainingArguments.RemoveAt(0);
                        }
                        else if (option.ParametersRequired)
                        {
                            // error
                            throw new ParseException("Option: " + name 
                                + ", missing reqired parametr");
                        }
                    }

                    if (parameterAlreadySet)
                    {
                        // store it as the option parameter
                        parameters.Add(parameter);
                        option.Action.Execute(parameters, parserState);
                    }

                    //process the option, give it the parameters
                }
                //    else (ie. argument is not an option)
                else
                {
                    throw new ParseException("Invalid option name:" + name);
                    //DEPRECATED
                    //        parsing should be stopped
                    //parserState.ContinueParsing = false;
                }
            }

            if (requiredOptions.Count != 0)
            {
                // error if not all reqired option was set
                string missingReqiredOptions = "";
                foreach (Option reqiredOption in requiredOptions.ToList())
                {
                    missingReqiredOptions += reqiredOption.ToString() + "; ";
                }
                throw new ParseException("Missing some required options: " 
                    + missingReqiredOptions);
            }

            return remainingArguments;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Represents the information about the program.
        /// </summary>
        public ProgramInformation ProgramInfo { get { return programInfo_; } }

        /// <summary>
        /// Indicates whether to add a default option to display the help.
        /// </summary>
        /// <seealso cref="HelpFormatter"/>
        public bool UseDefaultHelpOption { get; set; }

        /// <summary>
        /// Indicates whether to add a default option to display the help.
        /// </summary>
        /// <seealso cref="OptionParser.ProgramInformation"/>
        public bool UseDefaultVersionOption { get; set; }

        /// <summary>
        /// Indicates whether to automatically exit the whole program in the
        /// <c>Exit()</c> method. Eg. after printing the help message or
        /// program version.
        /// </summary>
        public bool ProgramExitEnabled { get; set; }

        /// <summary>
        /// Represents a formatter which can compile and format a help
        /// message about how to use the command line options.
        /// </summary>
        public HelpFormatter HelpFormatter
        {
            get { return helpFormatter_; }
            set { helpFormatter_ = value; }
        }

        #endregion

        #region Public inner classes

        /// <summary>
        /// Represents the information about the program such as its name or
        /// version and facilitates printing some help messages about the
        /// program usage.
        /// </summary>
        public sealed class ProgramInformation
        {
            /// <summary>
            /// Initializes the <see cref="ProgramInformation"/> class with the
            /// specified <see cref="OptionParser"/>.
            /// </summary>
            /// <param name="parser"></param>
            public ProgramInformation(OptionParser parser)
            {
                Name = System.IO.Path.GetFileName(
                    System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                UsageFormat = "{0} [options]";

                parser_ = parser;
            }

            /// <summary>
            /// Prints the compiled help message about program usage and its
            /// command line options to the specified output writer.
            /// </summary>
            /// <param name="writer">The output writer.</param>
            public void PrintHelp(System.IO.TextWriter writer)
            {
                parser_.HelpFormatter.FormatHelp(writer, parser_.options_.ToList(),
                    parser_.ProgramInfo);
            }

            /// <summary>
            /// Prints the message about program version to the specified
            /// output writer.
            /// </summary>
            /// <param name="writer">The output writer.</param>
            public void PrintVersionInfo(System.IO.TextWriter writer)
            {
                parser_.HelpFormatter.FormatVersion(writer, parser_.ProgramInfo);
            }

            /// <summary>
            /// Represents the program name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Represents the program version.
            /// </summary>
            public string Version { get; set; }

            /// <summary>
            /// Represents the format of the usage line as recognized by
            /// <c>String.Format</c>.
            /// </summary>
            /// <remarks>The "{0}" will be replaced by the program
            /// <see cref="Name"/>
            /// </remarks>
            /// <example>"{0} [options] MORE_ARGUMENTS"</example>
            public string UsageFormat { get; set; }

            /// <summary>
            /// The reference to the option parser.
            /// </summary>
            private OptionParser parser_;
        }

        /// <summary>
        /// Represents the current state of a parsing by the
        /// of the <c>ParseArguments()</c> method.
        /// </summary>
        public sealed class State
        {
            #region Construction

            /// <summary>
            /// Initializes the <see cref="State"/> class with a refence to the
            /// option parse.
            /// </summary>
            /// <param name="parser"></param>
            public State(OptionParser parser)
            {
                Parser = parser;
                ContinueParsing = true;
            }

            #endregion

            #region Public methods

            /// <summary>
            /// Returns the default argument parser for the specified type.
            /// </summary>
            /// <typeparam name="ValueType">The type of argument parser output.
            /// </typeparam>
            /// <returns>A default argument parser for the specified type.</returns>
            public ArgumentParser<ValueType> GetDefaultArgumentParser<ValueType>()
            {
                return argumentParserRegistry_.GetParser<ValueType>();
            }

            /// <summary>
            /// Exits the parser and optionally the whole program with the
            /// specified exit code depending on the
            /// <see cref="OptionParser.ProgramExitEnabled"/> flag.
            /// </summary>
            /// <param name="exitCode">The specified exit code.</param>
            public void Exit(int exitCode)
            {
                ContinueParsing = false;
                if (Parser.ProgramExitEnabled)
                {
                    Environment.Exit(exitCode);
                }
            }

            #endregion

            #region Public properties

            // set to false -> 
            /// <summary>
            /// Gets or sets the logical value whether to continue the parsing
            /// process (true) or stop it (false) considering the rest of them
            /// as positional arguments.
            /// </summary>
            /// <remarks>
            /// In contrast with <c>Exit()</c> method this just indicates
            /// the parsing process is finished and it does not try to
            /// exit the whole program.
            /// </remarks>
            public bool ContinueParsing { get; set; }

            /// <summary>
            /// Gets the reference to the option parser currently processing
            /// with this state.
            /// </summary>
            public OptionParser Parser { get; private set; }

            /// <summary>
            /// Gets or sets the registry of arguments parsers.
            /// </summary>
            public ArgumentParserRegistry ArgumentParserRegistry {
                get {
                    if (argumentParserRegistry_ == null) {
                        argumentParserRegistry_ = CreateDefaultArgumentParserRegistry();
                    }
                    return argumentParserRegistry_;
                }
                set { argumentParserRegistry_ = value; }
            }

            #endregion

            #region Private methods

            /// <summary>
            /// Creates an instance of <see cref="ArgumentParserRegistry"/> class
            /// and populate it with some common argument parsers.
            /// </summary>
            /// <returns></returns>
            private static ArgumentParserRegistry CreateDefaultArgumentParserRegistry()
            {
                ArgumentParserRegistry registry = new ArgumentParserRegistry();
                registry.RegisterParser<string>(new StringArgumentParser());
                registry.RegisterParser<int>(new Int32ArgumentParser());
                registry.RegisterParser<double>(new DoubleArgumentParser());
                registry.RegisterParser<bool>(new BoolArgumentParser());
                return registry;
            }

            #endregion

            #region Private fields

            private ArgumentParserRegistry argumentParserRegistry_;

            #endregion
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Defines some commonly used options like help, version or
        /// option terminator "--".
        /// </summary>
        /// <remarks>
        /// Defining a default help option depends on
        /// <see cref="UseDefaultHelpOption"/>, defining a default version
        /// option depends on <see cref="UseDefaultVersionOption"/>.
        /// </remarks>
        private void AddDefaultOptions()
        {
            AddOption(
                new Option(new string[] { "--" }, "Terminate the option list.",
                    new SimpleCallbackAction((OptionParser.State parserState) =>
                    {
                        parserState.ContinueParsing = false;
                    })
                ) { ParametersCount = 0 }
            );

            if (UseDefaultHelpOption)
            {
                Action callback = new SimpleCallbackAction(
                    (OptionParser.State parserState) =>
                    {
                        parserState.Parser.ProgramInfo.PrintHelp(Console.Out);
                        parserState.Exit(2);
                    });
                AddOption(new Option(
                        new string[] { "-h", "--help" },
                        "Print the help on program options.",
                        callback) { ParametersCount = 0 }
                    );
            }

            if (UseDefaultVersionOption)
            {
                Action callback = new SimpleCallbackAction(
                    (OptionParser.State parserState) =>
                    {
                        parserState.Parser.ProgramInfo.PrintVersionInfo(Console.Out);
                        parserState.Exit(2);
                    });
                AddOption(new Option(new string[] { "-V", "--version" },
                        "Print the program version.", callback)
                        { ParametersCount = 0 }
                    );
            }
        }

        #endregion

        #region Private fields

        private Options options_ = new Options();
        private HelpFormatter helpFormatter_;
        private ProgramInformation programInfo_;

        #endregion

        #region Private inner classes

        /// <summary>
        /// Storage for defined options accessible by their names (<see cref="Option.Names"/>).
        /// </summary>
        private sealed class Options
        {
            /// <summary>
            /// Define an option.
            /// </summary>
            /// <param name="option">option to be defined</param>
            /// <exception cref="ArgumentException">
            /// If any of the options name has been already defined by another option.</exception>
            /// <see cref="Option.Names"/>
            public void AddOption(Option option)
            {
                if (option.Names.Any(name => HasOption(name)))
                {
                    throw new ArgumentException(
                        "One of the option names is already defined by another option.");
                    // TODO: specify which name of which option is already defined
                }
                foreach (string name in option.Names)
                {
                    optionsMap_.Add(name, option);
                }
            }

            /// <summary>
            /// Return the option which was defined by its <c>name</c>
            /// (<see cref="Option.Names"/>).
            /// </summary>
            /// <param name="name">any of the option names</param>
            /// <returns>option having one of the names equal to <c>name</c></returns>
            public Option GetOption(string name)
            {
                return optionsMap_[name];
            }

            /// <summary>
            /// Check if an option with name equal to <c>name</c> was defined
            /// (<see cref="Option.Names"/>).
            /// </summary>
            /// <param name="name">any of the option names</param>
            /// <returns>true if an option having one of the names equal to
            /// <c>name</c> was defined</returns>
            public bool HasOption(string name)
            {
                return optionsMap_.ContainsKey(name);
            }

            /// <summary>
            /// Return the list of all defined options.
            /// </summary>
            /// <returns>list of all defined options</returns>
            public List<Option> ToList()
            {
                return optionsMap_.Values.ToList();
            }

            /// <summary>
            /// Represents the mapping between synonymous option names and
            /// option itself.
            /// </summary>
            private Dictionary<string, Option> optionsMap_ = new Dictionary<string, Option>();
        }

        #endregion
    }
}
