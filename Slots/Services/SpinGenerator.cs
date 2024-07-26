using Slots.Interfaces;
using Slots.Models;

namespace Slots.Services
{
    public class SpinGenerator : ISpinGenerator
    {
        private readonly IReelGenerator _reelGenerator;

        public SpinGenerator(IReelGenerator reelGenerator)
        {
            _reelGenerator = reelGenerator;
        }

        public SpinResult SpinResult(decimal stake)
        {
            var reels = _reelGenerator.GenerateReelPositions();

            return new SpinResult()
            {
                Stake = stake,
                ReelPositions = reels,
                Win = reels.Any(r => r.Win),
                Winnings = reels.Sum(r => r.Multiplier) * stake
            };
        }
    }
}
