using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SimpleCalculator.Core.BusinessLogic;
using SimpleCalculator.Core.BusinessLogic.TokenStrategies;
using SimpleCalculator.Core.Entities;
using SimpleCalculator.Core.Interfaces;
using Xunit;

namespace SimpleCalculator.Tests.BusinessLogic
{
    public class LexerTests
    {
        private static List<TokenDefinition> GivenAListOfTokenDefinitions()
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

            return tokenStrategies
                .Select(tokenStrategy => tokenStrategy.TokenDefinition)
                .ToList();
        }

        [Fact]
        public void ItShallThrowAnInvalidOperationExceptionForInvalidTokens()
        {
            // Given
            var tokenDefinitions = GivenAListOfTokenDefinitions();
            var sut = new Lexer(tokenDefinitions);

            // When
            Action action = () => sut.Tokenize("5;3");

            // Then
            Assert.Throws<InvalidOperationException>(action);
        }

        [Fact]
        public void ItShallTokenizeAnValidInput()
        {
            // Given
            var tokenDefinitions = GivenAListOfTokenDefinitions();
            var sut = new Lexer(tokenDefinitions);

            // When
            var tokens = sut.Tokenize("variable1 5= 4 + 3 - 4 * ~");

            // Then
            var actualTokenValues = tokens.Select(token => token.Value).ToArray();
            var expectedTokenValues = new []
            {
                "variable1",
                " ",
                "5",
                "=",
                " ",
                "4",
                " ",
                "+",
                " ",
                "3",
                " ",
                "-",
                " ",
                "4",
                " ",
                "*",
                " ",
                "~",
            };
            Assert.Equal(expectedTokenValues, actualTokenValues);
        }
    }
}