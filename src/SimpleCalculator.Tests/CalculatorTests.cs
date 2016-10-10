using System.Collections.Generic;
using System.Linq;
using SimpleCalculator.Core;
using SimpleCalculator.Core.BusinessLogic;
using SimpleCalculator.Core.BusinessLogic.TokenStrategies;
using SimpleCalculator.Core.Interfaces;
using Xunit;

namespace SimpleCalculator.Tests
{
    public class CalculatorTests
    {
        public CalculatorTests()
        {
            var tokenStrategies = new List<ITokenStrategy>
            {
                new WhitespaceTokenStrategy(),
                new NumberTokenStrategy(),
                new BinaryOperatorTokenStrategy(),
                new UnaryOperatorTokenStrategy(),
                new VariableTokenStrategy(),
                new AssignmentTokenStrategy()
            };
            var tokenDefinitions = tokenStrategies
                .Select(tokenStrategy => tokenStrategy.TokenDefinition)
                .ToList();

            var lexer = new Lexer(tokenDefinitions);
            var parser = new Parser(tokenStrategies);

            _sut = new Calculator(lexer, parser);
        }

        private readonly Calculator _sut;

        [Fact]
        public void ItShallAddAndSubtractAndMultiplyAndDivideMultipleNumbers()
        {
            // Given
            const string input = "7 15 + 2 * 11 /";

            // When
            var result = _sut.Calculate<int>(input);

            // Then
            Assert.Equal(4, result);
        }

        [Fact]
        public void ItShallAddAndSubtractMultipleNumbers()
        {
            // Given
            const string input = "7 6 3 2 1+--+";

            // When
            var result = _sut.Calculate<int>(input);

            // Then
            Assert.Equal(13, result);
        }

        [Fact]
        public void ItShallAddAndSubtractMultipleFloatNumbers()
        {
            // Given
            const string input = "8.4 7.2 - .68 + 4 /";

            // When
            var result = _sut.Calculate<double>(input);

            // Then
            Assert.Equal(0.47, result, 15);
        }
    }
}