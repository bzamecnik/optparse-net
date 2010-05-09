using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    /// <summary>
    /// Represents an action to be executed when an associated option is
    /// encountered among the command line arguments.
    /// </summary>
    public interface Action
    {
        /// <summary>
        /// Executes the action.
        /// </summary>
        /// <remarks>
        /// The option parameter is passed as a list. For now an option
        /// supports one or zero parameters. This can be seen from
        /// the <c>parameters.Count</c>.
        /// It is future-compatible to support multiple parameters per option.
        /// </remarks>
        /// <param name="parameters">The list of option parameters.</param>
        /// <param name="parserState">Current state of the option parser.</param>
        void Execute(IList<string> parameters, OptionParser.State parserState);
    }
}
