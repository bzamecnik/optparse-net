using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public interface HelpFormatter
    {
        void FormatHelp(System.IO.TextWriter writer, List<Option> options, OptionParser.ProgramInformation programInfo);
        void FormatVersion(System.IO.TextWriter writer, OptionParser.ProgramInformation programInfo);
    }
}
