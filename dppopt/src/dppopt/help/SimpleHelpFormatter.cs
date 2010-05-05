using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public class SimpleHelpFormatter : HelpFormatter
    {
        public void FormatHelp(System.IO.TextWriter writer, List<Option> options, OptionParser.ProgramInformation programInfo)
        {
            string usageLine = String.Format(programInfo.Usage, programInfo.Name);
            writer.WriteLine("Usage: {0}", usageLine);
            writer.WriteLine();
            writer.WriteLine("Options:");
            foreach (Option option in options) {
                writer.Write("  ");
                for (int i = 0; i < option.Names.Count; i++) {
                    if (i > 0) {
                        writer.Write(", ");
                    }
                    writer.Write(option.Names[i]);
                }
                writer.Write("  {0}", option.HelpText);
            }
        }

        public void FormatVersion(System.IO.TextWriter writer, OptionParser.ProgramInformation programInfo) {
            writer.WriteLine(String.Format("{0} {1}", programInfo.Name, programInfo.Version));
        }
    }
}
