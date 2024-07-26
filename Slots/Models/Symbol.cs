namespace Slots.Models
{
    public class Symbol
    {
        public string Name { get; set; } = string.Empty;
        public char Character { get; set; }
        public bool IsWildCard { get; set; } = false;
        public decimal Coefficient { get; set; }
        public decimal Probability { get; set; }
    }
}
