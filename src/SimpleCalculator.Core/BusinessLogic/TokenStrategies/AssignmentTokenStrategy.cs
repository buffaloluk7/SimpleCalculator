using System;
using System.Collections.Generic;
using SimpleCalculator.Core.Entities;
using SimpleCalculator.Core.Interfaces;

namespace SimpleCalculator.Core.BusinessLogic.TokenStrategies
{
    public class AssignmentTokenStrategy : ITokenStrategy
    {
        public TokenType TokenType { get; } = TokenType.Assignment;

        public void HandleToken(Token token, Stack<int> stack, Stack<Tuple<string, int>> symbolTable)
        {
            if (stack.Count == 0)
            {
                throw new InvalidOperationException("No value on stack which can be assigned to a variable.");
            }
            if (symbolTable.Count == 0)
            {
                throw new InvalidOperationException("No variable declared before assignment.");
            }

            var value = stack.Peek();
            var variable = symbolTable.Pop();

            symbolTable.Push(new Tuple<string, int>(variable.Item1, value));
        }
    }
}