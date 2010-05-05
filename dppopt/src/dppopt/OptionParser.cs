using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public class OptionParser
    {
        public OptionParser()
        {
            AddDefaultOptions();
        }

        public void AddOption(Option option)
        {
            options_.AddOption(option);
        }

        public List<string> ParseArguments(string[] arguments)
        {
            List<string> remainingArguments = new List<string>(arguments);
            // do parsing stuff ...

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


        public ProgramInformation ProgramInfo { get { return programInfo_; } }

        public HelpFormatter HelpFormatter
        {
            get { return helpFormatter_; }
            set { helpFormatter_ = value; }
        }

        private void ExitProgram()
        {
            // TODO - exitting strategy
        }

        private void Stop()
        {
            // TODO - stop parsing the input arguments
            // Consider the rest of them as positional arguments.
        }

        private void AddDefaultOptions()
        {
            AddOption(
                new Option(new string[] { "--" }, "Do not treat the rest of arguments as options",
                    new SimpleCallbackAction((OptionParser parser) =>
                    {
                        parser.Stop();
                    })
                )
            );

            AddOption(
                new Option(new string[] { "-h", "--help" },
                    "Print help about program options",
                    new SimpleCallbackAction((OptionParser parser) =>
                    {
                        parser.PrintHelp(Console.Out);
                        parser.ExitProgram();
                    })
                )
            );

            AddOption(
                new Option(new string[] { "-v", "--version" }, "Print program version",
                    new SimpleCallbackAction((OptionParser parser) =>
                    {
                        parser.PrintVersionInfo(Console.Out);
                        parser.ExitProgram();
                    })
                )
            );
        }

        private Options options_ = new Options();
        private HelpFormatter helpFormatter_ = new SimpleHelpFormatter();
        private ProgramInformation programInfo_ = new ProgramInformation();

        public class ProgramInformation
        {
            public string Name { get; set; }
            public string Version { get; set; }
            public string Usage { get; set; }
        }
    }
}
