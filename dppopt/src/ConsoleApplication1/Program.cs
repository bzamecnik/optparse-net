using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dppopt;

namespace dppoptTimeExample
{
    class Program
    {
        /*  TIME
            GNU Options
                -f FORMAT, --format=FORMAT
                    Specify output format, possibly overriding the format 
                    specified in the environment variable TIME.
                -p, --portability
                    Use the portable output format.
                -o FILE, --output=FILE
                    Do not send the results to stderr, but overwrite the 
                    specified file.
                -a, --append
                    (Used together with -o.) Do not overwrite but append.
                -v, --verbose
                    Give very verbose output about all the program knows about.

            GNU Standard Options
                -h --help 
                    Print a usage message on standard output and exit 
                    successfully.
                -V, --version
                    Print version information on standard output, then exit 
                    successfully.
                --     
                    Terminate option list.
        */
        static void Main(string[] args)
        {
           // Set parser
            OptionParser parser = new OptionParser();
            parser.ProgramInfo.Name = "Time";
            parser.ProgramInfo.Version = "3.14";

            // Add option, which has reqired parameter and meta variable "TIME"
            OptionValue<string> format = new OptionValue<string>("DD.MM.YYYY");
            parser.AddOption(
                new Option(new string[] { "-f", "--format" }, 
                    "Specify output format, possibly overriding the format specified in the environment variable TIME.",
                    new StoreAction<string>(format)
                ) { ParametersRequired = true, MetaVariable = "TIME" }
            );

            // Add option, which has not any parameter
            OptionValue<bool> portability = new OptionValue<bool>(false);
            parser.AddOption(
                new Option(new string[] { "-p", "--portability" }, 
                    "Use the portable output format.",
                    new StoreConstAction<bool>(portability, true)
                ) { ParametersCount = 0 }
            );

            // Add option, which has reqired parameter
            OptionValue<string> output = new OptionValue<string>();
            parser.AddOption(
                new Option(new string[] { "-o", "--output" }, 
                    "Do not send the results to stderr, but overwrite the specified file.",
                    new StoreAction<string>(output)
                ) { ParametersRequired = true}
            );

            // Add option, which has not any parameter
            OptionValue<bool> append = new OptionValue<bool>(false);
            parser.AddOption(
                new Option(new string[] { "-a", "--append" }, 
                    "(Used together with -o.) Do not overwrite but append.",
                    new StoreConstAction<bool>(append, true)
                ) { ParametersCount = 0 }
            );

            // Add option, which has not any parameter
            OptionValue<bool> verbose = new OptionValue<bool>(false);
            parser.AddOption(
                new Option(new string[] { "-v", "--verbose" }, 
                    "Give very verbose output about all the program knows about.",
                    new StoreConstAction<bool>(verbose, true)
                ) { ParametersCount = 0 }
            );

            try
            {
                // Run options parser (for example with args: -f YYYY.MM.DDD -p -o ./outputfile -a -v)
                List<string> remainingArgs = (List<string>)parser.ParseArguments(args);
            }
            catch (ParseException exception)
            {
                // User control the exception
                System.Console.WriteLine("exception: " + exception.ToString());
            }
        }
    }
}
