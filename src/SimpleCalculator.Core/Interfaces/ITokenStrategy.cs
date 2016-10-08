using System;
using System.Collections.Generic;
using SimpleCalculator.Core.Entities;

namespace SimpleCalculator.Core.Interfaces
{
    public interface ITokenStrategy
    {
        TokenType TokenType { get; }

        void HandleToken(Token token, Stack<int> stack, Stack<Tuple<string, int>> symbolTable);
    }
}