using Moq;
using Slots.Interfaces;
using Slots.Models;
using Slots.Services;

namespace SlotsTests.StepDefinitions
{
    [Binding]
    public class SlotsMachineStepDefinitions
    {
        private IReelSpinner? _reelSpinner;
        private Settings _settings;

        private readonly Mock<IDisplayAdapter> _testDisplay = new Mock<IDisplayAdapter>();
        private readonly Mock<IReelGenerator> _testReelGenerator = new Mock<IReelGenerator>();

        private IWallet _wallet = new InMemoryWallet();

        public SlotsMachineStepDefinitions()
        {
            _settings = new Settings();
        }

        [Given("the slot machine is running")]
        public void GivenTheSlotMachineIsRunning()
        {
            var spinner = new SpinGenerator(_testReelGenerator.Object);

            _reelSpinner = new ReelSpinner(spinner, _testDisplay.Object, _wallet);
        }

        [Given("the reel width is {int}")]
        public void GivenTheReelWidthIs(int reelWidth)
        {
            _settings.ReelWidth = reelWidth;
        }

        [Given("the line count is {int}")]
        public void GivenTheLineCountIs(int lineCount)
        {
            _settings.LineCount = lineCount;
        }

        [Given("the available symbols are:")]
        public void GivenTheAvailableSymbolsAre(DataTable dataTable)
        {
            _settings.Symbols = dataTable.CreateSet<Symbol>();
        }

        [Given("the player wallet has {int}")]
        public void GivenThePlayerWalletHas(int balance)
        {
            _wallet.AddFunds(balance);
        }

        [Given("the input stake is {float}")]
        public void GivenTheInputStakeIs(decimal stake)
        {
            _testDisplay.Setup(d => d.GetStake()).Returns(stake);
        }

        [Given("the reel spin is {string}")]
        public void GivenTheReelSpinIs(string spin)
        {
            var reels = spin.Split(',');

            var reelPositions = reels.Select(s =>
            {
                return new ReelPosition()
                {
                    Symbols = s.ToCharArray().Select(ToSymbol)
                }.CalculateWin();
            });

            _testReelGenerator
                .Setup(g => g.GenerateReelPositions())
                .Returns(reelPositions);
        }

        private Symbol ToSymbol(char symbol) =>
            _settings.Symbols.Where(s => s.Character == symbol).First();


        [When("we pull the handle")]
        public void WhenWePullTheHandle()
        {
            _reelSpinner?.Spin();
        }

        [Then("we should get a result out")]
        public void ThenWeShouldGetAResultOut()
        {
            _testDisplay.Verify(d => d.ShowSpinResult(It.Is<SpinResult>(r => r.ReelPositions.Any())));
        }

        [Then("we show the wallet")]
        public void ThenWeShowTheWallet()
        {
            _testDisplay.Verify(d => d.ShowWallet(It.IsAny<decimal>()));
        }

        [Then("the winnings displayed are {decimal}")]
        public void ThenTheWinningsDisplayedAre(decimal winnings)
        {
            _testDisplay.Verify(d => d.ShowSpinResult(It.Is<SpinResult>(r => r.Winnings == winnings)));
        }

        [Then("the final player wallet is {decimal}")]
        public void ThenTheFinalPlayerWalletIs(decimal balance)
        {
            _testDisplay.Verify(d => d.ShowWallet(It.Is<decimal>(w => w == balance)));
        }

        [Then("an error is shown")]
        public void ThenAnErrorIsShown()
        {
            _testDisplay.Verify(d => d.ShowError(It.IsAny<string>()));
        }

        [Then("we didn't take any money, so their wallet is still {decimal}")]
        public void ThenTheirWalletIsStill(decimal balance)
        {
            _wallet.GetBalance().Should().Be(balance);
        }

    }
}
