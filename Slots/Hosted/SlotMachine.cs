using Microsoft.Extensions.Hosting;
using Slots.Interfaces;
using Slots.Services;

namespace Slots.Hosted;

public class SlotMachine : IHostedService
{
    private readonly IReelSpinner _reelSpinner;
    private readonly IWallet _wallet;
    readonly IHostApplicationLifetime _hostLifetime;

    public SlotMachine(
        IReelSpinner reelSpinner, 
        IHostApplicationLifetime hostLifetime, 
        IWallet wallet)
    {
        _reelSpinner = reelSpinner;
        _hostLifetime = hostLifetime;
        _wallet = wallet;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Starting");
        Console.WriteLine("Filling player wallet");
        _wallet.AddFunds(200);

        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Run(_reelSpinner.Spin, cancellationToken);
        }

        _hostLifetime.StopApplication();
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task StopAsync(CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        Console.WriteLine("Stopping");
    }
}
