namespace Slots.Services
{
    public interface IWallet
    {
        public decimal GetBalance();
        void AddFunds(decimal delta);

        void TakeFunds(decimal delta);
    }

    public class InMemoryWallet : IWallet
    {
        private decimal Amount { get; set; } = 0;

        public decimal GetBalance()
        {
            return Amount;
        }

        public void AddFunds(decimal delta)
        {
            if (delta < 0) 
            {
                throw new ArgumentException("Cannot add negative amount");
            }

            Amount += delta;
        }

        public void TakeFunds(decimal delta)
        {
            if (delta > Amount)
            {
                throw new ArgumentException("Cannot take more than balance");
            }

            Amount -= delta;
        }
    }
}
