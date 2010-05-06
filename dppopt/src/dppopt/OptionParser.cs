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
            AddDefaultOptions();
        }

        #endregion

        #region Public methods

        public void AddOption(Option option)
        {
            options_.AddOption(option);
        }

        public List<string> ParseArguments(string[] arguments)
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
            //    if the next input argument is an option
            //        fetch the option
            //            in addition delete it from the remaining input arguments list
            //        identify the option
            //            error if the option is unknown or somehow bad
            //        get the number of its parameters
            //        if there is enough input arguments
            //            fetch the correct number of option's parameters
            //                in addition delete them from the remaining input arguments list
            //        else 
            //            if the parameters are not required
            //                use default parameter value
            //            else error
            //        process the option, give it the parameters
            //    else (ie. argument is not an option)
            //        parsing should be stopped
            // return the rest

            return remainingArguments;
        }

        public void PrintHelp(System.IO.TextWriter writer)
        {
            HelpFormatter.FormatHelp(writer, options_.ToList(), ProgramInfo);
        }

        public void PrintVersionInfo(System.IO.TextWriter writer)
        {
            HelpFormatter.FormatVersion(writer, ProgramInfo);
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
            public ProgramInformation()
            {
                Name = System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                Usage = Name + " [options]";
            }

            public string Name { get; set; }
            public string Version { get; set; }
            public string Usage { get; set; }
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
                    "Print help about program options",
                    new SimpleCallbackAction((OptionParser.State parserState) =>
                    {
                        parserState.Parser.PrintHelp(Console.Out);
                        parserState.Parser.Exit();
                    })
                )
            );

            // TODO: support enabling/disabling this option
            AddOption(
                new Option(new string[] { "-v", "--version" }, "Print program version.",
                    new SimpleCallbackAction((OptionParser.State parserState) =>
                    {
                        parserState.Parser.PrintVersionInfo(Console.Out);
                        parserState.Parser.Exit();
                    })
                )
            );
        }

        #endregion

        #region Private fields

        private Options options_ = new Options();
        private HelpFormatter helpFormatter_ = new SimpleHelpFormatter();
        private ProgramInformation programInfo_ = new ProgramInformation();

        #endregion

        #region Private inner classes

        private sealed class Options
        {
            public void AddOption(Option option)
            {
                foreach (string name in option.Names)
                {
                    optionsMap_.Add(name, option);
                }
            }

            public Option GetOption(string name)
            {
                return optionsMap_[name];
            }

            public bool HasOption(string name)
            {
                return optionsMap_.ContainsKey(name);
            }

            public List<Option> ToList()
            {
                return optionsMap_.Values.ToList();
            }

            private Dictionary<string, Option> optionsMap_ = new Dictionary<string, Option>();
        }

        #endregion
    }
}
