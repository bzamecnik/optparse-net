using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public abstract class ParametrizedAction<ValueType> : Action
    {
        #region Public methods

        public abstract void Execute(IList<string> parameters, OptionParser.State parserState);

        public ValueType ParseArgument(string argument)
        {
            ValueType parsedArgument = ParameterParser.ParseArgument(argument);
            if (!Filter.IsValid(parsedArgument)) {
                throw new ParseException("Argument does not conform to the filter: " + argument);
            }
            return parsedArgument;
        }

        #endregion

        #region Public properties

        public ValueFilter<ValueType> Filter
        {
            get { return filter_; }
            set { filter_ = value; }
        }

        public ArgumentParser<ValueType> ParameterParser
        {
            get
            {
                if (parameterParser_ == null)
                {
                    // Note: This might throw an InvalidArgumentParserException.
                    // TODO: It would be better to initialize the parser in the
                    // constructor and throw that exception from there
                    // (in definition time, not in parsing time).
                    // Or the exception could be thrown from ParseArgument()
                    parameterParser_ = ArgumentParserFactory.GetParser<ValueType>();
                }
                return parameterParser_;
            }
            set { parameterParser_ = value; }
        }

        #endregion

        #region Private fields

        private ArgumentParser<ValueType> parameterParser_;
        private ValueFilter<ValueType> filter_ = new EmptyValueFilter<ValueType>();

        #endregion
    }
}
