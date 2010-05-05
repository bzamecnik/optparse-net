using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dppopt
{
    public class ArgumentParserFactory
    {
        public static ArgumentParser<ValueType> GetParser<ValueType>() {
            Type valueType = typeof(ValueType);
            if (!knownParsers_.ContainsKey(valueType))
            {
                throw new InvalidArgumentParserException("No such a parser for that type ...");
            }
            object parserObject = knownParsers_[valueType];
            ArgumentParser<ValueType> parser = parserObject as ArgumentParser<ValueType>;
            if (parser == null) {
                throw new InvalidArgumentParserException("Invalid parser instance ...");
            }
            return parser;
        }

        static void RegisterParsers(Dictionary<Type, object> knownParsers) {
            knownParsers.Add(typeof(string), new StringArgumentParser());
            knownParsers.Add(typeof(int), new IntArgumentParser());
            knownParsers.Add(typeof(double), new DoubleArgumentParser());
            knownParsers.Add(typeof(bool), new BoolArgumentParser());
        }

        static Dictionary<Type, object> knownParsers_ = new Dictionary<Type, object>();
    }
}
