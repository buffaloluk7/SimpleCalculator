using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SimpleCalculator.Core.Entities;
using SimpleCalculator.Core.Interfaces;

namespace SimpleCalculator.Core.BusinessLogic.TokenStrategies
{
    public class WhitespaceTokenStrategy : ITokenStrategy
    {
        public TokenDefinition TokenDefinition { get; } = new TokenDefinition("whitespace", new Regex(@"[ \t\f\r\n]+"));

        public void HandleToken(Token token, Stack<int> stack, Stack<Tuple<string, int>> symbolTable)
        {
        }
    }
}