using Slots.Interfaces;

namespace Slots.Services
{
    public class ReelSpinner : IReelSpinner
    {
        private readonly ISpinGenerator _spinGenerator;
        private readonly IDisplayAdapter _display;
        private readonly IWallet _wallet;

        public ReelSpinner(
            ISpinGenerator spinGenerator,
            IDisplayAdapter display,
            IWallet wallet) 
        { 
            _display = display;
            _spinGenerator = spinGenerator;
            _wallet = wallet;
        }

        public void Spin()
        {
            try
            {
                var stake = _display.GetStake();
                if (stake <= 0)
                {
                    throw new ArgumentException("Stake muct be positive");
                }

                _wallet.TakeFunds(stake);

                var result = _spinGenerator.SpinResult(stake);
                if (result.Win)
                {
                    _wallet.AddFunds(result.Winnings);
                }

                _display.ShowSpinResult(result);
                _display.ShowWallet(_wallet.GetBalance());
                _display.Reset();
            }
            catch (Exception ex) 
            {
                _display.ShowError(ex.Message);
            }
        }

    }
}
