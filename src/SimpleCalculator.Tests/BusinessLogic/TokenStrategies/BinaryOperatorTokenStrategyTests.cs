using System;
using System.Collections.Generic;
using SimpleCalculator.Core.BusinessLogic.TokenStrategies;
using SimpleCalculator.Core.Entities;
using Xunit;

namespace SimpleCalculator.Tests.BusinessLogic.TokenStrategies
{
    public class BinaryOperatorTokenStrategyTests
    {
        private readonly BinaryOperatorTokenStrategy _sut = new BinaryOperatorTokenStrategy();

        private static Token GivenAnBinaryOperatorTokenWithAnInvalidOperatorSign()
        {
            return new Token("_", null);
        }

        private static Stack<int> GivenAnStackWithTwoElements()
        {
            var stack = new Stack<int>();
            stack.Push(42);
            stack.Push(43);

            return stack;
        }

        [Fact]
        public void ItShallThrowAnInvalidOperationExceptionForAnUnexpectedOperatorSign()
        {
            // Given
            var token = GivenAnBinaryOperatorTokenWithAnInvalidOperatorSign();
            var stack = GivenAnStackWithTwoElements();

            // When
            Action action = () => _sut.HandleToken(token, stack, null);

            // Then
            Assert.Throws<InvalidOperationException>(action);
        }
    }
}