namespace Slots.Models
{
    public class ReelPosition
    {
        public bool Win { private set; get; } = false;

        public decimal Multiplier { get; set; } = 0;

        public IEnumerable<Symbol> Symbols { get; set; } = new List<Symbol>();

        public ReelPosition CalculateWin()
        {
            Win = false;

            var nonWildCount = Symbols
                .Distinct()
                .Where(s => !s.IsWildCard)
                .Count();

            SetWin(nonWildCount is <= 1);

            return this;
        }

        private void SetWin(bool win)
        {
            Win = win;
            Multiplier = win
                ? Symbols.Sum(s => s.Coefficient)
                : 0;
        }
    }
}
