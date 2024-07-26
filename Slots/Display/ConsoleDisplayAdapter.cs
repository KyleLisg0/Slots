using Slots.Interfaces;
using Slots.Models;

namespace Slots.Display
{
    public class ConsoleDisplayAdapter : IDisplayAdapter
    {
        public decimal GetStake()
        {
            Console.WriteLine("Enter stake amount (decimal): ");
            var s = Console.ReadLine();

            if (!decimal.TryParse(s, out decimal stake))
            {
                throw new InvalidCastException();
            }

            return stake;
        }

        public void Reset()
        {
            Task.WaitAny(
                Task.Delay(10000),
                Task.Run(Console.ReadLine)
            );

            Console.Clear();
        }

        public void ShowError(string message)
        {
            Console.WriteLine($"Something went wrong ({message}), please try again \n");
        }

        public void ShowSpinResult(SpinResult spinResult)
        {
            Console.WriteLine("\n");

            foreach (var reel in spinResult.ReelPositions)
            {
                Console.WriteLine(reel.Symbols.Select(s => s.Character).ToArray());
            }

            if (spinResult.Win)
            {
                Console.WriteLine("You are a winner!");
                Console.WriteLine($"You won {spinResult.Winnings}");
                return;
            }

            Console.WriteLine("You did not win this time.");
        }

        public void ShowWallet(decimal walletAmount)
        {
            Console.WriteLine("\n");
            Console.WriteLine($"Current balance is: {walletAmount}");
        }
    }
}
