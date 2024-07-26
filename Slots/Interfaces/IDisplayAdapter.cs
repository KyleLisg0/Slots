using Slots.Models;

namespace Slots.Interfaces
{
    public interface IDisplayAdapter
    {
        public void ShowSpinResult(SpinResult spinResult);
        public decimal GetStake();
        public void ShowError(string message);
        public void Reset();
        public void ShowWallet(decimal walletAmount);
    }
}
