using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
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
            programInfo_ = new ProgramInformation(this);
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
            State parsingState = new State(this);

            // do parsing stuff ...

            // TODO:
            // handle parameters like:
            //   --number=42
            // probably even like this:
            //   -n42
            // to do that preprocess the input arguments list or handle it in the main loop

            // while (there are any remaining input arguments) and (parsing should not be stopped)
            //    if the next input argument starts like an option
            //        fetch the argument
            //        identify the option
            //            error if the option is unknown or somehow bad
            //        delete the option from the remaining input arguments list
            //        get the number of parameters it might take
            //        if there is any parameter immediately after the option in the same argument
            //            store it as the option parameter
            //        if there is not a valid number of parameters after the option
            //            if the option should take some parameters and there are any in the input list
            //                fetch one argument and delete it from the list
            //                store it as the option parameter
            //            else
            //                error
            //        process the option, give it the parameters
            //    else (ie. argument is not an option)
            //        parsing should be stopped
            // return the rest

            return remainingArguments;
        }

        #endregion

        #region Public properties

        public ProgramInformation ProgramInfo { get { return programInfo_; } }

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
            public State(OptionParser parser)
            {
                Parser = parser;
                ContinueParsing = true;
            }

            // set to false -> consider the rest of them as positional arguments
            public bool ContinueParsing { get; set; }

            public OptionParser Parser { get; private set; }
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

            // TODO: support enabling/disabling this option
            AddOption(
                new Option(new string[] { "-h", "--help" },
                    "Print the help on program options.",
                    new SimpleCallbackAction((OptionParser.State parserState) =>
                    {
                        parserState.Parser.ProgramInfo.PrintHelp(Console.Out);
                        parserState.Parser.Exit();
                    })
                )
            );

            // TODO: support enabling/disabling this option
            AddOption(
                new Option(new string[] { "-v", "--version" }, "Print the program version.",
                    new SimpleCallbackAction((OptionParser.State parserState) =>
                    {
                        parserState.Parser.ProgramInfo.PrintVersionInfo(Console.Out);
                        parserState.Parser.Exit();
                    })
                )
            );
        }

        #endregion

        #region Private fields

        private Options options_ = new Options();
        private HelpFormatter helpFormatter_ = new SimpleHelpFormatter();
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
