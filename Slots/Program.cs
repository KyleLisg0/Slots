using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Slots.Display;
using Slots.Hosted;
using Slots.Interfaces;
using Slots.Models;
using Slots.Services;

PrintWelcomeMessage();

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<SlotMachine>();
builder.Services.AddSingleton<IReelSpinner, ReelSpinner>();
builder.Services.AddSingleton<IWallet, InMemoryWallet>();
builder.Services.AddScoped<ISpinGenerator, SpinGenerator>();
builder.Services.AddScoped<IReelGenerator, ReelGenerator>();
builder.Services.AddScoped<IDisplayAdapter, ConsoleDisplayAdapter>();

Settings settings = new Settings();
builder.Configuration.Bind(settings);
builder.Services.AddSingleton(settings);

using var host = builder.Build();

await host.RunAsync();

void PrintWelcomeMessage()
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("""
    ____________________
     | |*** Slots ***| |
     | |     |  |    | |
     ___________________
    """);
    Console.ResetColor();
}