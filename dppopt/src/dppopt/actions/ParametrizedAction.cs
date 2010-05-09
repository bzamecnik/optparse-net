using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    /// <summary>
    /// Represents an action with takes the option parameters.
    /// </summary>
    /// <typeparam name="ValueType">Type of an option parameter.</typeparam>
    public abstract class ParametrizedAction<ValueType> : Action
    {
        #region Public methods

        #region Interface Action

        // TODO: <inheritdoc />
        public abstract void Execute(IList<string> parameters, OptionParser.State parserState);

        #endregion

        /// <summary>
        /// Converts the string argument into its value in the specified type
        /// and checks it with a <see cref="Filter"/>.
        /// </summary>
        /// <param name="argument">An option argument to convert from string.</param>
        /// <returns>An instance of <see cref="ValueType"/> equivalent to its
        /// string representaion in <c>argument</c></returns>
        /// <exception cref="ParseException">
        /// <c>argument</c> is not in correct format or does not conform to
        /// the <see cref="Filter"/>
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <c>argument</c> is null
        /// </exception>
        public ValueType ParseArgument(string argument)
        {
            ValueType parsedArgument;
            try
            {
                parsedArgument = ParameterParser.ParseArgument(argument);
            }
            catch (FormatException exception)
            {
                throw new ParseException("Argument is in wrong format: " + argument, exception);
            }
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

        public ArgumentParser<ValueType> ParameterParser{ get; set; }

        #endregion

        #region Private methods

        /// <summary>
        /// Ensures that <see cref="ParameterParser"/> holds a correct parser
        /// even if none was defined.
        /// </summary>
        /// <remarks>
        /// If there was no parser defined in ParameterParser, this method
        /// tries to obtain a default parser registered for the <c>ValueType</c>
        /// in the <see cref="OptionParser.State"/>.
        /// </remarks>
        /// <param name="parserState">State of the OptionParser during parsing.</param>
        /// <exception cref="InvalidArgumentParserException">
        /// there is no associated argument parser for type <c>ValueType</c>
        /// </exception>
        private void EnsureParameterParser(OptionParser.State parserState) {
            if (ParameterParser == null) {
                ParameterParser = parserState.ArgumentParserFactory.GetParser<ValueType>();
            }
        }

        #endregion

        #region Private fields

        private ValueFilter<ValueType> filter_ = new EmptyValueFilter<ValueType>();

        #endregion
    }
}
