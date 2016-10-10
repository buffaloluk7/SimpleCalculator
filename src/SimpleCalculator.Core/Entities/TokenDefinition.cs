using System.Text.RegularExpressions;

namespace SimpleCalculator.Core.Entities
{
    public class TokenDefinition
    {
        public TokenDefinition(string tokenType, Regex regex)
        {
            TokenType = tokenType;
            Regex = regex;
        }

        public string TokenType { get; }
        public Regex Regex { get; }
    }
}