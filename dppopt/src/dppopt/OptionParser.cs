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

            // do parsing stuff ...

            // TODO:
            // handle parameters like:
            //   --number=42
            // probably even like this:
            //   -n42
            // to do that preprocess the input arguments list or handle it in the main loop
            bool isParsingStopped = false;

            // while (there are any remaining input arguments) and (parsing should not be stopped)
            while (remainingArguments.Count() > 0 && isParsingStopped == true)
            {
                string name = remainingArguments.First();
                string parameter;

                /*
                // handle parameters like:
                //   --number=42
                string pattern = @"(--[a-zA-Z0-9-]+)=(.*)";
                if (Regex.IsMatch(name, pattern))
                {
                    Match match = Regex.Match(name, pattern);
                    name = match.Groups[1].Value;
                    parameter = match.Groups[2].Value;
                }
                else if ()
                {
                
                }
                //*/

                //    if the next input argument starts like an option
                if (Option.IsValidOptionName(name))
                {
                    //        fetch the argument
                    //        identify the option
                    Option option;
                    if (!options_.HasOption(name))
                    {
                        //        error if the option is unknown or somehow bad
                        throw new ArgumentException("Invalid option name:" + name);
                    }
                    option = options_.GetOption(name);
                    //        delete the option from the remaining input arguments list
                    remainingArguments.Remove(name);

                    //        get the number of parameters it might take
                    //DEPRECATED

                    //        if there is any parameter immediately after the option in the same argument
                    //            store it as the option parameter
                    //TODO - how to?

                    //        if there is not a valid number of parameters after the option
                    //DEPRECATED


                    //            if the option should take some parameters and there are any in the input list
                    List<string> parameters = new List<string>();

                    if (option.ParametersCount > 0 && remainingArguments.Count() > 0)
                    {
                        //                fetch one argument and delete it from the list
                        parameter = remainingArguments.First();

                        if (!options_.HasOption(parameter))
                        {
                            remainingArguments.Remove(parameter);
                            parameters.Add(parameter);

                            //                store it as the option parameter
                            option.Action.Execute(parameters, parserState);
                        }
                        else if (option.ParametersRequired)
                        {
                            //                error
                            throw new ArgumentException("Option: " + name + ", missing reqired parametr");
                        }
                    }

                    
                    //            else
                    //        process the option, give it the parameters
                }
                //    else (ie. argument is not an option)
                else
                {
                    //        parsing should be stopped
                    isParsingStopped = true;
                }
            }
            // return the rest


            //TODO kontrola povinnych voleb
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
                return argumentParserFactory_.GetParser<ValueType>();
            }

            #endregion

            #region Public properties

            // set to false -> consider the rest of them as positional arguments
            public bool ContinueParsing { get; set; }

            public OptionParser Parser { get; private set; }

            public ArgumentParserFactory ArgumentParserFactory {
                get {
                    if (argumentParserFactory_ == null) {
                        argumentParserFactory_ = CreateDefaultArgumentParserFactory();
                    }
                    return argumentParserFactory_;
                }
                set { argumentParserFactory_ = value; }
            }

            #endregion

            #region Private methods

            private static ArgumentParserFactory CreateDefaultArgumentParserFactory()
            {
                ArgumentParserFactory factory = new ArgumentParserFactory();
                factory.RegisterParser<string>(new StringArgumentParser());
                factory.RegisterParser<int>(new IntArgumentParser());
                factory.RegisterParser<double>(new DoubleArgumentParser());
                factory.RegisterParser<bool>(new BoolArgumentParser());
                return factory;
            }
            #endregion

            #region Private fields

            private ArgumentParserFactory argumentParserFactory_;

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
                        "One of the options name is already defined by another option.");
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
