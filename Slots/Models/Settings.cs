namespace Slots.Models
{
    public class Settings
    {
        public IEnumerable<Symbol> Symbols { get; set; } = new List<Symbol>();
        public int ReelWidth { get; set; }
        public int LineCount { get; set;}
    }
}
