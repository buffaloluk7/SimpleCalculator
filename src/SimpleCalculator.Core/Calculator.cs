using SimpleCalculator.Core.BusinessLogic;

namespace SimpleCalculator.Core
{
    public class Calculator<T>
    {
        private readonly Lexer _lexer;
        private readonly Parser<T> _parser;

        public Calculator(Lexer lexer, Parser<T> parser)
        {
            _lexer = lexer;
            _parser = parser;
        }

        public T Calculate(string input)
        {
            var tokens = _lexer.Tokenize(input);
            var result = _parser.ParseTokens(tokens);

            return result;
        }
    }
}