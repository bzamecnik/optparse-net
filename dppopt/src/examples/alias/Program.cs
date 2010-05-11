using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dppopt;

namespace dppoptAliasExample
{
    class Program
    {
        /*  ALIAS
            GNU Options
                -d=DIR, --dir=DIR
                    Install the script into directory DIR, rather than
                    searching for a suitable directory in $PATH. 
                -m, --manpage
                    Display the manpage for the alias script given as the 
                    single argument. The alias can be an absolute pathname, or
                    the name of a script in $PATH. If the argument isn't an
                    alias script, or if multiple arguments are given, then all
                    arguments are passed to the system 'man' command. This
                    allows you to alias your man command like this:
                    alias man='0alias --manpage' 
                -r, --resolve
                    Print the interface URI for the given alias script to stdout

            GNU Standard Options
                -h, --help
                    Show the built-in help text. 
                -V, --version
                    Display version information.
        */
        static void Main(string[] args)
        {
            // Set parser
            OptionParser parser = new OptionParser();
            parser.ProgramInfo.Name = "Alias";
            parser.ProgramInfo.Version = "1.3.2.1";

            // Add reqired option, which has reqired parameter
            OptionValue<string> dir = new OptionValue<string>();
            parser.AddOption(
                new Option(new string[] { "-d", "--dir" },
                    "Install the script into directory DIR, rather than searching for a suitable directory in $PATH.",
                    new StoreAction<string>(dir)
                ) { Required = true, ParametersRequired = true}
            );

            // Add option, which has not any parameter
            OptionValue<bool> manpage = new OptionValue<bool>(false);
            parser.AddOption(
                new Option(new string[] { "-m", "--manpage" },
                    "Display the manpage for the alias script given as the single argument.",
                    new StoreConstAction<bool>(manpage, true)
                ) { ParametersCount = 0 }
            );

            // Add option, which has not any parameter
            OptionValue<bool> resolve = new OptionValue<bool>(false);
            parser.AddOption(
                new Option(new string[] { "-r", "--resolve" },
                    "Print the interface URI for the given alias script to stdout.",
                    new StoreConstAction<bool>(resolve, true)
                ) { ParametersCount = 0 }
            );

            try
            {
                // Run options parser (for example with args: --dir ./rootdir/nextdir/ -r)
                IList<string> remainingArgs = parser.ParseArguments(args);
                Console.WriteLine("dir: {0}", dir);
                Console.WriteLine("manpage: {0}", manpage);
                Console.WriteLine("resolve: {0}", resolve);
            }
            catch (ParseException exception)
            {
                // User control the exception
                System.Console.WriteLine("exception: " + exception.ToString());
            }
        }
    }
}
