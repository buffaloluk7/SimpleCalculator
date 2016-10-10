using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SimpleCalculator.Core.Entities;

namespace SimpleCalculator.Core.BusinessLogic
{
    public class Lexer
    {
        private readonly IList<TokenDefinition> _tokenDefinitions;
        private readonly Regex _tokenRegex;

        public Lexer(IList<TokenDefinition> tokenDefinitions)
        {
            _tokenDefinitions = tokenDefinitions;
            var regexGroups = _tokenDefinitions
                .Select(tokenDefinition => $"(?<{tokenDefinition.TokenType}>{tokenDefinition.Regex})")
                .ToList();
            var joinedRegexGroups = string.Join("|", regexGroups);
            _tokenRegex = new Regex($"^({joinedRegexGroups})");
        }

        public IList<Token> Tokenize(string input)
        {
            var tokens = new List<Token>();
            var remainingInput = input;

            while (remainingInput.Length > 0)
            {
                var match = _tokenRegex.Match(remainingInput);
                if (!match.Success)
                {
                    throw new InvalidOperationException($"No matching token definition found for {remainingInput}.");
                }

                var matchingTokenDefintion = _tokenDefinitions
                    .Single(tokenDefinition => match.Groups[tokenDefinition.TokenType.ToString()].Success);
                tokens.Add(new Token(match.Value, matchingTokenDefintion));

                remainingInput = remainingInput.Substring(match.Length);
            }

            return tokens;
        }
    }
}