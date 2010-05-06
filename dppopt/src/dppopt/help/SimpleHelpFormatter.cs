using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public class SimpleHelpFormatter : HelpFormatter
    {
        #region Public methods

        #region Interface HelpFormatter

        public void FormatHelp(System.IO.TextWriter writer, List<Option> options, OptionParser.ProgramInformation programInfo)
        {
            string usageLine = String.Format(programInfo.Usage, programInfo.Name);
            writer.WriteLine("Usage: {0}", usageLine);
            writer.WriteLine();
            writer.WriteLine("Options:");
            foreach (Option option in options)
            {
                writer.WriteLine("  {0} {1}", String.Join(", ", option.Names.ToArray()), option.HelpText);
            }
        }

        public void FormatVersion(System.IO.TextWriter writer, OptionParser.ProgramInformation programInfo)
        {
            writer.WriteLine(String.Format("{0} {1}", programInfo.Name, programInfo.Version));
        }

        #endregion

        #endregion
    }
}
