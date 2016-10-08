using System.Text.RegularExpressions;

namespace SimpleCalculator.Core.Entities
{
    public class TokenDefinition
    {
        public TokenDefinition(TokenType tokenType, Regex regex, bool ignoreToken = false)
        {
            TokenType = tokenType;
            Regex = regex;
            IgnoreToken = ignoreToken;
        }

        public TokenType TokenType { get; }
        public Regex Regex { get; }
        public bool IgnoreToken { get; }
    }
}