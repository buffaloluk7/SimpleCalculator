using System;
using System.Collections.Generic;
using SimpleCalculator.Core.BusinessLogic.TokenStrategies;
using SimpleCalculator.Core.Entities;
using Xunit;

namespace SimpleCalculator.Tests.BusinessLogic.TokenStrategies
{
    public class AssignmentTokenStrategyTests
    {
        private readonly AssignmentTokenStrategy _sut = new AssignmentTokenStrategy();

        private static Token GivenAnAssignmentToken()
        {
            return new Token("=", null);
        }

        private static Stack<int> GivenAnStackWithTwoElements()
        {
            var stack = new Stack<int>();
            stack.Push(42);
            stack.Push(43);

            return stack;
        }

        private static Stack<int> GivenAnEmptyStack()
        {
            return new Stack<int>();
        }

        private static Stack<Tuple<string, int>> GivenAnSymbolTableWithTwoElements()
        {
            var stack = new Stack<Tuple<string, int>>();
            stack.Push(new Tuple<string, int>("var1", 42));
            stack.Push(new Tuple<string, int>("var2", 43));

            return stack;
        }

        private static Stack<Tuple<string, int>> GivenAnEmptySymbolTable()
        {
            return new Stack<Tuple<string, int>>();
        }

        [Fact]
        public void ItShallThrowAnInvalidOperationExceptionForAnEmptyStack()
        {
            // Given
            var token = GivenAnAssignmentToken();
            var stack = GivenAnEmptyStack();
            var symbolTable = GivenAnSymbolTableWithTwoElements();

            // When
            Action action = () => _sut.HandleToken(token, stack, symbolTable);

            // Then
            Assert.Throws<InvalidOperationException>(action);
        }

        [Fact]
        public void ItShallThrowAnInvalidOperationExceptionForAnSymbolTable()
        {
            // Given
            var token = GivenAnAssignmentToken();
            var stack = GivenAnStackWithTwoElements();
            var symbolTable = GivenAnEmptySymbolTable();

            // When
            Action action = () => _sut.HandleToken(token, stack, symbolTable);

            // Then
            Assert.Throws<InvalidOperationException>(action);
        }
    }
}