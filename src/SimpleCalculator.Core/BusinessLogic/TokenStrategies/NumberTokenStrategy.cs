using System;
using System.Collections.Generic;
using SimpleCalculator.Core.Entities;
using SimpleCalculator.Core.Interfaces;

namespace SimpleCalculator.Core.BusinessLogic.TokenStrategies
{
    public class NumberTokenStrategy : ITokenStrategy
    {
        public TokenType TokenType { get; } = TokenType.Number;

        public void HandleToken(Token token, Stack<int> stack, Stack<Tuple<string, int>> symbolTable)
        {
            stack.Push(int.Parse(token.Value));
        }
    }
}