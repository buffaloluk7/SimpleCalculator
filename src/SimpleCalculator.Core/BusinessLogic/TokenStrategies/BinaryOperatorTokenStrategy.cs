using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SimpleCalculator.Core.Entities;
using SimpleCalculator.Core.Interfaces;

namespace SimpleCalculator.Core.BusinessLogic.TokenStrategies
{
    public class BinaryOperatorTokenStrategy : ITokenStrategy
    {
        public TokenDefinition TokenDefinition { get; } = new TokenDefinition("binary", new Regex(@"[\+\-\*\/]"));

        public void HandleToken(Token token, Stack<int> stack, Stack<Tuple<string, int>> symbolTable)
        {
            // Swap first and second argument (important for - and / operations)
            var secondArgument = stack.Pop();
            var firstArgument = stack.Pop();
            var result = ApplyBinaryOperation(firstArgument, secondArgument, token.Value);

            stack.Push(result);
        }

        private static int ApplyBinaryOperation(int firstArgument, int secondArgument, string @operator)
        {
            switch (@operator)
            {
                case "+":
                    return firstArgument + secondArgument;
                case "-":
                    return firstArgument - secondArgument;
                case "*":
                    return firstArgument*secondArgument;
                case "/":
                    return firstArgument/secondArgument;
                default:
                    throw new InvalidOperationException($"Invalid operator {@operator}.");
            }
        }
    }
}