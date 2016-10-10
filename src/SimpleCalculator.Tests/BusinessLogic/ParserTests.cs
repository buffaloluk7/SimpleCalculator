using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SimpleCalculator.Core.BusinessLogic;
using SimpleCalculator.Core.BusinessLogic.TokenStrategies;
using SimpleCalculator.Core.Entities;
using SimpleCalculator.Core.Interfaces;
using Xunit;

namespace SimpleCalculator.Tests.BusinessLogic
{
    public class ParserTests
    {
        public ParserTests()
        {
            var tokenStrategies = new List<ITokenStrategy>
            {
                new NumberTokenStrategy(),
                new UnaryOperatorTokenStrategy(),
                new BinaryOperatorTokenStrategy(),
                new VariableTokenStrategy(),
                new AssignmentTokenStrategy()
            };

            _sut = new Parser(tokenStrategies);
        }

        private readonly Parser _sut;

        private static IList<Token> GivenAListOfTokensContainingWhitespace()
        {
            var whitespaceTokenDefinition = new WhitespaceTokenStrategy().TokenDefinition;

            return new List<Token>
            {
                new Token(" ", whitespaceTokenDefinition)
            };
        }

        private static List<Token> GivenAListOfTokensContainingNumbersAndPlusSignsAndMinusSigns()
        {
            var numberTokenDefinition = new NumberTokenStrategy().TokenDefinition;
            var binaryOperatorTokenDefinition = new BinaryOperatorTokenStrategy().TokenDefinition;

            return new List<Token>
            {
                new Token("4", numberTokenDefinition),
                new Token("5", numberTokenDefinition),
                new Token("2", numberTokenDefinition),
                new Token("+", binaryOperatorTokenDefinition),
                new Token("+", binaryOperatorTokenDefinition),
                new Token("4", numberTokenDefinition),
                new Token("-", binaryOperatorTokenDefinition)
            };
        }

        private static List<Token> GivenAListOfTokensContainingNumbersAndMulitplicationSignsAndDivisionSigns()
        {
            var numberTokenDefinition = new NumberTokenStrategy().TokenDefinition;
            var binaryOperatorTokenDefinition = new BinaryOperatorTokenStrategy().TokenDefinition;

            return new List<Token>
            {
                new Token("12", numberTokenDefinition),
                new Token("2", numberTokenDefinition),
                new Token("2", numberTokenDefinition),
                new Token("*", binaryOperatorTokenDefinition),
                new Token("/", binaryOperatorTokenDefinition)
            };
        }

        private static List<Token> GivenAListOfTokensContainingNumbersAndMulitplicationSignsAndNegationSigns()
        {
            var numberTokenDefinition = new NumberTokenStrategy().TokenDefinition;
            var binaryOperatorTokenDefinition = new BinaryOperatorTokenStrategy().TokenDefinition;
            var unaryOperatorTokenDefinition = new UnaryOperatorTokenStrategy().TokenDefinition;

            return new List<Token>
            {
                new Token("12", numberTokenDefinition),
                new Token("2", numberTokenDefinition),
                new Token("~", unaryOperatorTokenDefinition),
                new Token("2", numberTokenDefinition),
                new Token("*", binaryOperatorTokenDefinition),
                new Token("~", unaryOperatorTokenDefinition),
                new Token("/", binaryOperatorTokenDefinition),
                new Token("~", unaryOperatorTokenDefinition)
            };
        }

        private static List<Token> GivenAListOfTokensContainingNumbersAndPlusSignsAndVariablesAndAssignments()
        {
            var numberTokenDefinition = new NumberTokenStrategy().TokenDefinition;
            var binaryOperatorTokenDefinition = new BinaryOperatorTokenStrategy().TokenDefinition;
            var variableTokenDefinition = new VariableTokenStrategy().TokenDefinition;
            var assignmentTokenDefinition = new AssignmentTokenStrategy().TokenDefinition;

            return new List<Token>
            {
                new Token("i", variableTokenDefinition),
                new Token("3", numberTokenDefinition),
                new Token("=", assignmentTokenDefinition),
                new Token("i", variableTokenDefinition),
                new Token("+", binaryOperatorTokenDefinition)
            };
        }

        private static List<Token> GivenAnInvalidTokenListContainingTooLittleBinaryOperators()
        {
            var numberTokenDefinition = new NumberTokenStrategy().TokenDefinition;
            var binaryOperatorTokenDefinition = new BinaryOperatorTokenStrategy().TokenDefinition;

            return new List<Token>
            {
                new Token("12", numberTokenDefinition),
                new Token("2", numberTokenDefinition),
                new Token("2", numberTokenDefinition),
                new Token("*", binaryOperatorTokenDefinition)
            };
        }

        private static List<Token> GivenAnInvalidTokenListContainingTooLittleNumbers()
        {
            var numberTokenDefinition = new TokenDefinition("number", new Regex(@"[0-9]+"));
            var binaryOperatorTokenDefinition = new TokenDefinition("binary", new Regex(@"[\+\-\*\/]"));

            return new List<Token>
            {
                new Token("12", numberTokenDefinition),
                new Token("3", numberTokenDefinition),
                new Token("+", binaryOperatorTokenDefinition),
                new Token("*", binaryOperatorTokenDefinition)
            };
        }

        [Fact]
        public void ItShallAddAndSubtractMultipleNumbers()
        {
            // Given
            var tokens = GivenAListOfTokensContainingNumbersAndPlusSignsAndMinusSigns();

            // When
            var result = _sut.ParseTokens(tokens);

            // Then
            Assert.Equal(7, result);
        }

        [Fact]
        public void ItShallApplyTheNegationOperator()
        {
            // Given
            var tokens = GivenAListOfTokensContainingNumbersAndMulitplicationSignsAndNegationSigns();

            // When
            var result = _sut.ParseTokens(tokens);

            // Then
            Assert.Equal(-3, result);
        }

        [Fact]
        public void ItShallAssignANumberToAnVariableAndAddItToAnotherNumber()
        {
            // Given
            var tokens = GivenAListOfTokensContainingNumbersAndPlusSignsAndVariablesAndAssignments();

            // When
            var result = _sut.ParseTokens(tokens);

            // Then
            Assert.Equal(6, result);
        }

        [Fact]
        public void ItShallMultiplyAndDivideMultipleNumbers()
        {
            // Given
            var tokens = GivenAListOfTokensContainingNumbersAndMulitplicationSignsAndDivisionSigns();

            // When
            var result = _sut.ParseTokens(tokens);

            // Then
            Assert.Equal(3, result);
        }

        [Fact]
        public void ItShallThrowAnInvalidOperationExceptionForAnWhitespaceToken()
        {
            // Given
            var tokens = GivenAListOfTokensContainingWhitespace();

            // When
            Action action = () => _sut.ParseTokens(tokens);

            // Then
            Assert.Throws<InvalidOperationException>(action);
        }

        [Fact]
        public void ItShallThrowAnInvalidOperationExceptionWhenParsingTokenListContainingTooLittleBinaryOperators()
        {
            // Given
            var tokens = GivenAnInvalidTokenListContainingTooLittleBinaryOperators();

            // When
            Action action = () => _sut.ParseTokens(tokens);

            // Then
            Assert.Throws<InvalidOperationException>(action);
        }

        [Fact]
        public void ItShallThrowAnInvalidOperationExceptionWhenParsingTokenListContainingTooLittleNumbers()
        {
            // Given
            var tokens = GivenAnInvalidTokenListContainingTooLittleNumbers();

            // When
            Action action = () => _sut.ParseTokens(tokens);

            // Then
            Assert.Throws<InvalidOperationException>(action);
        }
    }
}