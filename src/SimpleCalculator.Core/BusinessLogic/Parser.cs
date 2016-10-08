﻿using System;
using System.Collections.Generic;
using System.Linq;
using SimpleCalculator.Core.Entities;
using SimpleCalculator.Core.Interfaces;

namespace SimpleCalculator.Core.BusinessLogic
{
    public class Parser
    {
        private readonly IList<ITokenStrategy> _tokenStrategies;

        public Parser(IList<ITokenStrategy> tokenStrategies)
        {
            _tokenStrategies = tokenStrategies;
        }

        public int ParseTokens(IList<Token> tokens)
        {
            var stack = new Stack<int>();
            var symbolTable = new Stack<Tuple<string, int>>();

            foreach (var token in tokens)
            {
                var tokenType = token.MatchingTokenDefinition.TokenType;
                var strategy = _tokenStrategies.SingleOrDefault(s => s.TokenType == tokenType);
                if (strategy == null)
                {
                    throw new InvalidOperationException($"No strategy found for token type {tokenType}.");
                }

                strategy.HandleToken(token, stack, symbolTable);
            }

            if (stack.Count != 1)
            {
                throw new InvalidOperationException($"Stack contains {stack.Count} elements, expected: 1.");
            }

            return stack.Pop();
        }
    }
}