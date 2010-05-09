using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public sealed class ArgumentParserFactory
    {
        #region Public methods

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

        public void RegisterParser<ValueType>(ArgumentParser<ValueType> argumentParser)
        {
            knownParsers_.Add(typeof(ValueType), argumentParser);
        }

        #endregion

        #region Private fields

        private Dictionary<Type, object> knownParsers_ = new Dictionary<Type, object>();

        #endregion
    }
}
