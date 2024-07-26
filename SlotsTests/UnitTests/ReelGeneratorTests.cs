using Slots.Interfaces;
using Slots.Models;
using Slots.Services;

namespace SlotsTests.UnitTests
{
    public class ReelGeneratorTests
    {
        [Theory, InlineData(2, 4)]
        public void ReelGenerator_GenerateReelPositions_ShouldHaveCorrectColumnAndWidthCounts(int reelWidth, int lineCount)
        {
            var settings = new Settings();
            settings.ReelWidth = reelWidth;
            settings.LineCount = lineCount;
            settings.Symbols = new List<Symbol>()
            { 
                new() { Character = 'A', Coefficient = 1, Probability = 0.5M },
                new() { Character = 'B', Coefficient = 2, Probability = 0.5M },
            };

            ReelGenerator generator = new ReelGenerator(settings);

            var result = generator.GenerateReelPositions();

            result.Select(r => r.Symbols.Count().Should().Be(reelWidth));
            result.Should().HaveCount(lineCount);
        }
    }
}
