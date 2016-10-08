using System.Collections.Generic;
using System.Text.RegularExpressions;
using SimpleCalculator.Core;
using SimpleCalculator.Core.BusinessLogic;
using SimpleCalculator.Core.BusinessLogic.TokenStrategies;
using SimpleCalculator.Core.Entities;
using SimpleCalculator.Core.Interfaces;
using Xunit;

namespace SimpleCalculator.Tests
{
    public class CalculatorTests
    {
        public CalculatorTests()
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

            _sut = new Calculator(lexer, parser);
        }

        private readonly Calculator _sut;

        [Fact]
        public void ItShallAddAndSubtractAndMultiplyAndDivideMultipleNumbers()
        {
            // Given
            const string input = "7 15 + 2 * 11 /";

            // When
            var result = _sut.Calculate(input);

            // Then
            Assert.Equal(4, result);
        }

        [Fact]
        public void ItShallAddAndSubtractMultipleNumbers()
        {
            // Given
            const string input = "7 6 3 2 1+--+";

            // When
            var result = _sut.Calculate(input);

            // Then
            Assert.Equal(13, result);
        }
    }
}