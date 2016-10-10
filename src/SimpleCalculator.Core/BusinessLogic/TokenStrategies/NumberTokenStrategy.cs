using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SimpleCalculator.Core.Entities;
using SimpleCalculator.Core.Interfaces;

namespace SimpleCalculator.Core.BusinessLogic.TokenStrategies
{
    public class NumberTokenStrategy : ITokenStrategy
    {
        public TokenDefinition TokenDefinition { get; } = new TokenDefinition("number", new Regex(@"[0-9]+"));

        public void HandleToken(Token token, Stack<int> stack, Stack<Tuple<string, int>> symbolTable)
        {
            stack.Push(int.Parse(token.Value));
        }
    }
}