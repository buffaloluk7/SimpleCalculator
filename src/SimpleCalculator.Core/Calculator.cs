﻿using SimpleCalculator.Core.BusinessLogic;

namespace SimpleCalculator.Core
{
    public class Calculator
    {
        private readonly Lexer _lexer;
        private readonly Parser _parser;

        public Calculator(Lexer lexer, Parser parser)
        {
            _lexer = lexer;
            _parser = parser;
        }

        public int Calculate(string input)
        {
            var tokens = _lexer.Tokenize(input);
            var result = _parser.ParseTokens(tokens);

            return result;
        }
    }
}