using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    /// <summary>
    /// Represents a mapping from various types to argument parsers
    /// which can parse into these types.
    /// </summary>
    /// <remarks>
    /// An argument parser can convert values of a specified type from their
    /// string representaions into that type. The registry provides
    /// a mapping from a type to a parser that can parse values of that type.
    /// First, the mapping between type and a respectiveargument parser is
    /// defined by registering them with <c>RegisterParser&lt;ValueType&gt;</c>
    /// method. Then a proper parser can be obtained for a specified type by
    /// querying the registry with the <c>GetParser&lt;ValueType&gt;</c>
    /// method. An <see cref="InvalidArgumentParserException"/> might be
    /// thrown as a result of an incorrect query.
    /// </remarks>
    /// <seealso cref="ArgumentParser&lt;ValueType&gt;"/>
    public sealed class ArgumentParserRegistry
    {
        #region Public methods

        /// <summary>
        /// Obtains an argument parser that can parse string-represented
        /// values of the type in specified <c>ValueType</c>.
        /// </summary>
        /// <typeparam name="ValueType">The type of the desired parser output.
        /// </typeparam>
        /// <returns>An argument parser can convert values of <c>ValueType</c>
        /// type from their string representaions into that type.</returns>
        /// <exception cref="InvalidArgumentParserException">
        /// there is no parser registered for the specified type or the
        /// registered parser cannot convert strings into the specified type
        /// </exception>
        public ArgumentParser<ValueType> GetParser<ValueType>()
        {
            Type valueType = typeof(ValueType);
            if (!knownParsers_.ContainsKey(valueType))
            {
                throw new InvalidArgumentParserException("No such a parser for that type ...");
            }
            object parserObject = knownParsers_[valueType];
            ArgumentParser<ValueType> parser = parserObject as ArgumentParser<ValueType>;
            if (parser == null)
            {
                throw new InvalidArgumentParserException("Invalid parser instance ...");
            }
            return parser;
        }

        /// <summary>
        /// Defines a mapping between <c>ValueType</c> and <c>argumentParser</c>
        /// that can be later queried.
        /// </summary>
        /// <typeparam name="ValueType">The desired return type of the parser.
        /// </typeparam>
        /// <param name="argumentParser">The argument parser itself.</param>
        public void RegisterParser<ValueType>(ArgumentParser<ValueType> argumentParser)
        {
            knownParsers_.Add(typeof(ValueType), argumentParser);
        }

        #endregion

        #region Private fields

        /// <summary>
        /// A mapping between a type and an instance of
        /// ArgumentParser&lt;ValueType&gt;.
        /// </summary>
        /// <remarks>
        /// <see cref="object"/> is used as a base class because is
        /// <c>ArgumentParser&lt;ValueType&gt;</c> cannot be a base class.</remarks>
        private Dictionary<Type, object> knownParsers_ = new Dictionary<Type, object>();

        #endregion
    }
}
