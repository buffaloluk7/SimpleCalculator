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

        public void HandleToken<T>(Token token, Stack<T> stack, Stack<Tuple<string, T>> symbolTable)
        {
            // Swap first and second operand (important for - and / operations)
            var rightOperand = stack.Pop();
            var leftOperand = stack.Pop();
            var result = ApplyBinaryOperation(leftOperand, rightOperand, token.Value);

            stack.Push(result);
        }

        private static T ApplyBinaryOperation<T>(T leftOperand, T rightOperand, string @operator)
        {
            dynamic leftDynamicOperand = leftOperand;

            switch (@operator)
            {
                case "+":
                    return leftDynamicOperand + rightOperand;
                case "-":
                    return leftDynamicOperand - rightOperand;
                case "*":
                    return leftDynamicOperand*rightOperand;
                case "/":
                    return leftDynamicOperand/rightOperand;
                default:
                    throw new InvalidOperationException($"Invalid operator {@operator}.");
            }
        }
    }
}