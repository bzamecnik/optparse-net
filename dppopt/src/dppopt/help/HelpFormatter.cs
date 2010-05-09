using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    // TODO: rename: HelpFormatter -> IHelpFormatter

    /// <summary>
    /// A strategy for formatting help about using the program options to an output stream.
    /// </summary>
    public interface HelpFormatter
    {
        /// <summary>
        /// Format help about using options to an output stream.
        /// </summary>
        /// <param name="writer">output stream</param>
        /// <param name="options">information about the options</param>
        /// <param name="programInfo">information about the program</param>
        void FormatHelp(
            System.IO.TextWriter writer,
            IList<Option> options,
            OptionParser.ProgramInformation programInfo
            );

        /// <summary>
        /// Format program version to an output stream.
        /// </summary>
        /// <param name="writer">output stream</param>
        /// <param name="programInfo">information about the program</param>
        void FormatVersion(
            System.IO.TextWriter writer,
            OptionParser.ProgramInformation programInfo
            );
    }
}
