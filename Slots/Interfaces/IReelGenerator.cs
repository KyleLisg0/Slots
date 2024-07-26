using Slots.Models;

namespace Slots.Interfaces
{
    public interface IReelGenerator
    {
        IEnumerable<ReelPosition> GenerateReelPositions();
    }
}
