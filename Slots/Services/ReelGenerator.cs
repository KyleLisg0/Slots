using Slots.Interfaces;
using Slots.Models;

namespace Slots.Services
{
    public class ReelGenerator : IReelGenerator
    {
        private int _reelWidth = 3;
        private int _lineCount = 4;

        private Symbol[] _symbols;

        public ReelGenerator(
            Settings settings) 
        { 
            _reelWidth = settings.ReelWidth;
            _lineCount = settings.LineCount;
            _symbols = settings.Symbols.ToArray();
        }

        public IEnumerable<ReelPosition> GenerateReelPositions() =>
            new ReelPosition[_lineCount].Select(r => r = GenerateLine());

        private ReelPosition GenerateLine()
        {
            return new ReelPosition()
            {
                Symbols = GenerateSymbols(_reelWidth)
            }.CalculateWin();
        }

        private IEnumerable<Symbol> GenerateSymbols(int count) =>
            new Symbol[count].Select(s => s = GenerateSymbol());

        private Symbol GenerateSymbol()
        {
            var orderedSymbols = _symbols.OrderBy(s => s.Probability).ToArray();

            Random rnd = new Random();
            var result = Convert.ToDecimal(rnd.NextDouble());

            decimal rollingProbability = 0M;
            for (int i = 0; i < orderedSymbols.Length; i++)
            {
                rollingProbability += orderedSymbols[i].Probability;
                if (result < rollingProbability)
                {
                    return orderedSymbols[i];
                }
            }

            throw new ArgumentOutOfRangeException("Result was outside of configured probabilities - make sure your symbol probabilities add to 1");
        }
    }
}
