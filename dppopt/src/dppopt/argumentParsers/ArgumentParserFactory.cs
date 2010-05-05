using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public sealed class ArgumentParserFactory
    {
        #region Public methods

        public static ArgumentParser<ValueType> GetParser<ValueType>()
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

        public static void RegisterParser<ValueType>(ArgumentParser<ValueType> argumentParser)
        {
            RegisterParser<ValueType>(knownParsers_, argumentParser);
        }

        #endregion

        #region Private methods

        private static void RegisterParser<ValueType>(
            Dictionary<Type, object> knownParsers,
            ArgumentParser<ValueType> argumentParser)
        {
            knownParsers.Add(typeof(ValueType), argumentParser);
        }

        private static Dictionary<Type, object> RegisterParsers()
        {
            Dictionary<Type, object> knownParsers = new Dictionary<Type, object>();
            RegisterParser<string>(knownParsers, new StringArgumentParser());
            RegisterParser<int>(knownParsers, new IntArgumentParser());
            RegisterParser<double>(knownParsers, new DoubleArgumentParser());
            RegisterParser<bool>(knownParsers, new BoolArgumentParser());
            return knownParsers;
        }

        #endregion

        #region Private fields

        private static Dictionary<Type, object> knownParsers_ = RegisterParsers();

        #endregion
    }
}
