namespace Slots.Models
{
    public class SpinResult
    {
        public decimal Stake { get; set; }
        public bool Win { get; set; }
        public decimal Winnings { get; set; }
        public IEnumerable<ReelPosition> ReelPositions { get; set; } = new List<ReelPosition>();
    }
}
