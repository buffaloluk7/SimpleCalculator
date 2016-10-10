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

        public void HandleToken<T>(Token token, Stack<T> stack, Stack<Tuple<string, T>> symbolTable)
        {
            var operand = stack.Pop();
            var result = ApplyUnaryOperation(operand, token.Value);

            stack.Push(result);
        }

        private static T ApplyUnaryOperation<T>(T operand, string @operator)
        {
            dynamic dynamicOperand = operand;

            switch (@operator)
            {
                case "~":
                    return -dynamicOperand;
                default:
                    throw new InvalidOperationException($"Invalid operator {@operator}.");
            }
        }
    }
}