using System;
using System.Collections.Generic;
using SimpleCalculator.Core.Entities;

namespace SimpleCalculator.Core.Interfaces
{
    public interface ITokenStrategy
    {
        TokenDefinition TokenDefinition { get; }

        void HandleToken<T>(Token token, Stack<T> stack, Stack<Tuple<string, T>> symbolTable);
    }
}