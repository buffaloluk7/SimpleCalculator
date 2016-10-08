using System;
using System.Collections.Generic;
using System.Linq;
using SimpleCalculator.Core.Entities;
using SimpleCalculator.Core.Interfaces;

namespace SimpleCalculator.Core.BusinessLogic.TokenStrategies
{
    public class VariableTokenStrategy : ITokenStrategy
    {
        public TokenType TokenType { get; } = TokenType.Variable;

        public void HandleToken(Token token, Stack<int> stack, Stack<Tuple<string, int>> symbolTable)
        {
            var variable = symbolTable.SingleOrDefault(v => v.Item1 == token.Value);
            if (variable == null)
            {
                symbolTable.Push(new Tuple<string, int>(token.Value, 0));
            }
            else
            {
                stack.Push(variable.Item2);
            }
        }
    }
}