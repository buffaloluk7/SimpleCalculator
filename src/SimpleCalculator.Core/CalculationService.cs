using System.Collections.Generic;
using System.Text.RegularExpressions;
using SimpleCalculator.Core.BusinessLogic;
using SimpleCalculator.Core.BusinessLogic.TokenStrategies;
using SimpleCalculator.Core.Entities;
using SimpleCalculator.Core.Interfaces;

namespace SimpleCalculator.Core
{
    public class CalculationService
    {
        private readonly Calculator _calculator;

        public CalculationService()
        {
            var lexer = new Lexer(new List<TokenDefinition>
            {
                new TokenDefinition(TokenType.Whitespace, new Regex(@"[ \t\f\r\n]+"), true),
                new TokenDefinition(TokenType.Number, new Regex(@"[0-9]+")),
                new TokenDefinition(TokenType.BinaryOperator, new Regex(@"[\+\-\*\/]")),
                new TokenDefinition(TokenType.UnaryOperator, new Regex(@"~")),
                new TokenDefinition(TokenType.Variable, new Regex(@"[a-zA-Z][a-zA-Z0-9_]*")),
                new TokenDefinition(TokenType.Assignment, new Regex(@"="))
            });
            var parser = new Parser(new List<ITokenStrategy>
            {
                new NumberTokenStrategy(),
                new UnaryOperatorTokenStrategy(),
                new BinaryOperatorTokenStrategy(),
                new VariableTokenStrategy(),
                new AssignmentTokenStrategy()
            });

            _calculator = new Calculator(lexer, parser);
        }

        public int Calculate(string input)
        {
            return _calculator.Calculate(input);
        }
    }
}