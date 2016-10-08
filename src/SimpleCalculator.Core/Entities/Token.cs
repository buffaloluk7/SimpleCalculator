namespace SimpleCalculator.Core.Entities
{
    public class Token
    {
        public Token(string value, TokenDefinition matchingTokenDefinition)
        {
            Value = value;
            MatchingTokenDefinition = matchingTokenDefinition;
        }

        public string Value { get; set; }
        public TokenDefinition MatchingTokenDefinition { get; }
    }
}