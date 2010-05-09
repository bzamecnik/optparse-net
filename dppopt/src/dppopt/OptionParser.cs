using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace dppopt
{
    // TODO: mohlo by se jmenovat spis CommandLineParser

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

        public OptionParser()
        {
            helpFormatter_ = new SimpleHelpFormatter();
            programInfo_ = new ProgramInformation(this);
            UseDefaultHelpOption = true;
            UseDefaultVersionOption = true;
            AddDefaultOptions();
        }

        #endregion

        #region Public methods

        public void AddOption(Option option)
        {
            options_.AddOption(option);
        }

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

                string patternLong = @Option.RegularExpressions.LongOptionWithParam;
                string patternShort = @Option.RegularExpressions.ShortOptionWithParam;
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

        public ProgramInformation ProgramInfo { get { return programInfo_; } }
        public bool UseDefaultHelpOption { get; set; }
        public bool UseDefaultVersionOption { get; set; }

        public HelpFormatter HelpFormatter
        {
            get { return helpFormatter_; }
            set { helpFormatter_ = value; }
        }

        #endregion

        #region Public inner classes

        public sealed class ProgramInformation
        {
            public ProgramInformation(OptionParser parser)
            {
                Name = System.IO.Path.GetFileName(
                    System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                UsageFormat = "{0} [options]";

                parser_ = parser;
            }

            public void PrintHelp(System.IO.TextWriter writer)
            {
                parser_.HelpFormatter.FormatHelp(writer, parser_.options_.ToList(),
                    parser_.ProgramInfo);
            }

            public void PrintVersionInfo(System.IO.TextWriter writer)
            {
                parser_.HelpFormatter.FormatVersion(writer, parser_.ProgramInfo);
            }

            public string Name { get; set; }
            public string Version { get; set; }
            public string UsageFormat { get; set; }

            private OptionParser parser_;
        }

        public sealed class State
        {
            #region Construction

            public State(OptionParser parser)
            {
                Parser = parser;
                ContinueParsing = true;
            }

            #endregion

            #region Public methods

            public ArgumentParser<ValueType> GetDefaultArgumentParser<ValueType>()
            {
                return argumentParserRegistry_.GetParser<ValueType>();
            }

            #endregion

            #region Public properties

            // set to false -> consider the rest of them as positional arguments
            public bool ContinueParsing { get; set; }

            public OptionParser Parser { get; private set; }

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

        private void Exit()
        {
            // TODO - exitting strategy - exit the program or just the parser
        }

        private void AddDefaultOptions()
        {
            AddOption(
                new Option(new string[] { "--" }, "Terminate the option list.",
                    new SimpleCallbackAction((OptionParser.State parserState) =>
                    {
                        parserState.ContinueParsing = false;
                    })
                )
            );

            if (UseDefaultHelpOption)
            {
                Action callback = new SimpleCallbackAction(
                    (OptionParser.State parserState) =>
                    {
                        parserState.Parser.ProgramInfo.PrintHelp(Console.Out);
                        parserState.Parser.Exit();
                    });
                AddOption(new Option(
                        new string[] { "-h", "--help" },
                        "Print the help on program options.",
                        callback)
                    );
            }

            if (UseDefaultVersionOption)
            {
                Action callback = new SimpleCallbackAction(
                    (OptionParser.State parserState) =>
                    {
                        parserState.Parser.ProgramInfo.PrintVersionInfo(Console.Out);
                        parserState.Parser.Exit();
                    });
                AddOption(new Option(new string[] { "-v", "--version" },
                        "Print the program version.", callback) { Required = true }
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

            private Dictionary<string, Option> optionsMap_ = new Dictionary<string, Option>();
        }

        #endregion
    }
}
