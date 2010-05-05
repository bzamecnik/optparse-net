using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public abstract class ParametrizedAction<ValueType> : Action
    {
        public abstract void Execute(List<string> arguments, OptionParser parser);

        public ValueFilter<ValueType> Filter { get; set; }

        public ArgumentParser<ValueType> ParameterParser
        {
            get { return parameterParser_; }
            set { parameterParser_ = value; }
        }

        public ValueType ParseArgument(string argument) {
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

        private ArgumentParser<ValueType> parameterParser_ = ParameterParserFactory.GetParser(typeof(ValueType));
    }
}
