using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SimpleCalculator.Core.Entities;
using SimpleCalculator.Core.Interfaces;

namespace SimpleCalculator.Core.BusinessLogic.TokenStrategies
{
    public class VariableTokenStrategy : ITokenStrategy
    {
        public TokenDefinition TokenDefinition { get; } = new TokenDefinition("variable",
            new Regex(@"[a-zA-Z][a-zA-Z0-9_]*"));

        public void HandleToken<T>(Token token, Stack<T> stack, Stack<Tuple<string, T>> symbolTable)
        {
            var variable = symbolTable.SingleOrDefault(v => v.Item1 == token.Value);
            if (variable == null)
            {
                symbolTable.Push(new Tuple<string, T>(token.Value, default(T)));
            }
            else
            {
                stack.Push(variable.Item2);
            }
        }
    }
}