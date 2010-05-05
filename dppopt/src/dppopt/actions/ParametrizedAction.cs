using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public abstract class ParametrizedAction<ValueType> : Action
    {
        #region Public methods

        public abstract void Execute(List<string> arguments, OptionParser.State parserState);

        public ValueType ParseArgument(string argument)
        {
            // TODO

            // ValueType parsedArgument = ... parse argument according to its type ...
            //if (!Filter.IsValid(parsedArgument)) {
            //    throw ParseException("Invalid argument: ...");
            //}

            return default(ValueType);
        }

        public List<ValueType> ParseArguments(List<string> arguments)
        {
            // TODO
            return new List<ValueType>();
        }

        #endregion

        #region Public properties

        public ValueFilter<ValueType> Filter { get; set; }

        public ArgumentParser<ValueType> ParameterParser
        {
            get
            {
                if (parameterParser_ == null)
                {
                    parameterParser_ = ArgumentParserFactory.GetParser<ValueType>();
                }
                return parameterParser_;
            }
            set { parameterParser_ = value; }
        }

        #endregion

        #region Private fields

        // TODO: this might throw an InvalidArgumentParserException
        // Probably use a factory method for actions.
        private ArgumentParser<ValueType> parameterParser_;

        #endregion
    }
}
