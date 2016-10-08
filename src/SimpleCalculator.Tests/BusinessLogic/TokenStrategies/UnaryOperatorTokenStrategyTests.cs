using System;
using System.Collections.Generic;
using SimpleCalculator.Core.BusinessLogic.TokenStrategies;
using SimpleCalculator.Core.Entities;
using Xunit;

namespace SimpleCalculator.Tests.BusinessLogic.TokenStrategies
{
    public class UnaryOperatorTokenStrategyTests
    {
        private readonly UnaryOperatorTokenStrategy _sut = new UnaryOperatorTokenStrategy();

        private static Token GivenAnUnaryOperatorTokenWithAnInvalidOperatorSign()
        {
            return new Token("_", null);
        }

        private static Stack<int> GivenAnStackWithOneElement()
        {
            var stack = new Stack<int>();
            stack.Push(42);

            return stack;
        }

        [Fact]
        public void ItShallThrowAnInvalidOperationExceptionForAnUnexpectedOperatorSign()
        {
            // Given
            var token = GivenAnUnaryOperatorTokenWithAnInvalidOperatorSign();
            var stack = GivenAnStackWithOneElement();

            // When
            Action action = () => _sut.HandleToken(token, stack, null);

            // Then
            Assert.Throws<InvalidOperationException>(action);
        }
    }
}