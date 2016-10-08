using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SimpleCalculator.Core.BusinessLogic;
using SimpleCalculator.Core.Entities;
using Xunit;

namespace SimpleCalculator.Tests.BusinessLogic
{
    public class LexerTests
    {
        private static List<TokenDefinition> GivenAListOfTokenDefinitions()
        {
            return new List<TokenDefinition>
            {
                new TokenDefinition(TokenType.Whitespace, new Regex(@"[ \t\f\r\n]+"), true),
                new TokenDefinition(TokenType.Number, new Regex(@"[0-9]+")),
                new TokenDefinition(TokenType.BinaryOperator, new Regex(@"[\+\-\*\/]")),
                new TokenDefinition(TokenType.UnaryOperator, new Regex(@"~")),
                new TokenDefinition(TokenType.Variable, new Regex(@"[a-zA-Z][a-zA-Z0-9_]*")),
                new TokenDefinition(TokenType.Assignment, new Regex(@"="))
            };
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
            Assert.Equal(10, tokens.Count);
            Assert.Equal("variable1", tokens[0].Value);
            Assert.Equal("5", tokens[1].Value);
            Assert.Equal("=", tokens[2].Value);
            Assert.Equal("4", tokens[3].Value);
            Assert.Equal("+", tokens[4].Value);
            Assert.Equal("3", tokens[5].Value);
            Assert.Equal("-", tokens[6].Value);
            Assert.Equal("4", tokens[7].Value);
            Assert.Equal("*", tokens[8].Value);
            Assert.Equal("~", tokens[9].Value);
        }
    }
}