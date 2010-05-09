using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    /// <summary>
    /// A simple help formatter without any sofisticated indentation or line
    /// wrapping.
    /// </summary>
    public class SimpleHelpFormatter : HelpFormatter
    {
        #region Public methods

        #region Interface HelpFormatter

        /// <summary>
        /// Formats a help message about using options to an output stream.
        /// Write a general usage pattern and a description of each option
        /// (<see cref="Option.Names"/>, <see cref="Option.HelpText"/>
        /// </summary>
        /// <param name="writer">output stream</param>
        /// <param name="options">information about the options</param>
        /// <param name="programInfo">information about the program</param>
        public void FormatHelp(
            System.IO.TextWriter writer,
            IList<Option> options,
            OptionParser.ProgramInformation programInfo)
        {
            string usageLine = String.Format(programInfo.UsageFormat, programInfo.Name);
            writer.WriteLine("Usage: {0}", usageLine);
            writer.WriteLine();
            writer.WriteLine("Options:");
            foreach (Option option in options)
            {
                // TODO: print the meta variable if needed (or do that in
                // a more sophisticated HelpFormatter)
                writer.WriteLine("  {0} {1}",
                    String.Join(", ", option.Names.ToArray()), option.HelpText);
            }
        }


        /// <summary>
        /// Formats program version string to an output stream.
        /// </summary>
        /// <param name="writer">The output writer.</param>
        /// <param name="programInfo">The information about the program.</param>
        public void FormatVersion(
            System.IO.TextWriter writer,
            OptionParser.ProgramInformation programInfo)
        {
            writer.WriteLine(String.Format("{0} {1}", programInfo.Name, programInfo.Version));
        }

        #endregion

        #endregion
    }
}
