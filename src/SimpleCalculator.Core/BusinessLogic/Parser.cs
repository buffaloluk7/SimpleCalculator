using System;
using System.Collections.Generic;
using System.Linq;
using SimpleCalculator.Core.Entities;
using SimpleCalculator.Core.Interfaces;

namespace SimpleCalculator.Core.BusinessLogic
{
    public class Parser<T>
    {
        private readonly IList<ITokenStrategy> _tokenStrategies;
        private readonly Stack<Tuple<string, T>> _symbolTable;

        public Parser(IList<ITokenStrategy> tokenStrategies)
        {
            _tokenStrategies = tokenStrategies;
            _symbolTable = new Stack<Tuple<string, T>>();
        }

        public T ParseTokens(IList<Token> tokens)
        {
            var stack = new Stack<T>();

            foreach (var token in tokens)
            {
                var tokenType = token.MatchingTokenDefinition.TokenType;
                var strategy = _tokenStrategies.SingleOrDefault(s => s.TokenDefinition.TokenType == tokenType);
                if (strategy == null)
                {
                    throw new InvalidOperationException($"No strategy found for token type {tokenType}.");
                }

                strategy.HandleToken(token, stack, _symbolTable);
            }

            if (stack.Count != 1)
            {
                throw new InvalidOperationException($"Stack contains {stack.Count} elements, expected: 1.");
            }

            return stack.Pop();
        }
    }
}