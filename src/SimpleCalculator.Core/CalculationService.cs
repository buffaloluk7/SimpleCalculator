using System.Collections.Generic;
using System.Linq;
using SimpleCalculator.Core.BusinessLogic;
using SimpleCalculator.Core.BusinessLogic.TokenStrategies;
using SimpleCalculator.Core.Interfaces;

namespace SimpleCalculator.Core
{
    public class CalculationService
    {
        private readonly Calculator _calculator;

        public CalculationService()
        {
            var tokenStrategies = new List<ITokenStrategy>
            {
                new WhitespaceTokenStrategy(),
                new NumberTokenStrategy(),
                new BinaryOperatorTokenStrategy(),
                new UnaryOperatorTokenStrategy(),
                new VariableTokenStrategy(),
                new AssignmentTokenStrategy()
            };
            var tokenDefinitions = tokenStrategies
                .Select(tokenStrategy => tokenStrategy.TokenDefinition)
                .ToList();

            var lexer = new Lexer(tokenDefinitions);
            var parser = new Parser(tokenStrategies);

            _calculator = new Calculator(lexer, parser);
        }

        public T Calculate<T>(string input)
        {
            return _calculator.Calculate<T>(input);
        }
    }
}