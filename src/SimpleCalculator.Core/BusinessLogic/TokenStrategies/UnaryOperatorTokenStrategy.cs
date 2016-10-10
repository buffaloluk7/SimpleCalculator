using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SimpleCalculator.Core.Entities;
using SimpleCalculator.Core.Interfaces;

namespace SimpleCalculator.Core.BusinessLogic.TokenStrategies
{
    public class UnaryOperatorTokenStrategy : ITokenStrategy
    {
        public TokenDefinition TokenDefinition { get; } = new TokenDefinition("unary", new Regex(@"~"));

        public void HandleToken(Token token, Stack<int> stack, Stack<Tuple<string, int>> symbolTable)
        {
            var argument = stack.Pop();
            var result = ApplyUnaryOperation(argument, token.Value);

            stack.Push(result);
        }

        private static int ApplyUnaryOperation(int argument, string @operator)
        {
            switch (@operator)
            {
                case "~":
                    return -argument;
                default:
                    throw new InvalidOperationException($"Invalid operator {@operator}.");
            }
        }
    }
}