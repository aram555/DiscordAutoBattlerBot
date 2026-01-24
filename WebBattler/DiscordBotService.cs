using System.Reflection;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace WebBattler.Services;

public class DiscordBotService : BackgroundService
{
    private readonly DiscordSocketClient _client;
    private readonly InteractionService _interactions;
    private readonly IServiceProvider _services;
    private readonly IConfiguration _config;

    public DiscordBotService(
        DiscordSocketClient client,
        InteractionService interactions,
        IServiceProvider services,
        IConfiguration config)
    {
        _client = client;
        _interactions = interactions;
        _services = services;
        _config = config;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _client.Log += LogAsync;
        _interactions.Log += LogAsync;

        await _client.LoginAsync(
            TokenType.Bot,
            _config["Discord:Token"]);

        await _client.StartAsync();

        _client.Ready += OnReadyAsync;
        _client.InteractionCreated += HandleInteractionAsync;

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    private async Task OnReadyAsync()
    {
        // Регистрируем команды (глобально или на гильдию)
        await _interactions.AddModulesAsync(
            typeof(WebBattler.Services.Modules.DiscordModulesMarker).Assembly,
            _services);

        // ⚠ для разработки — гильдия (мгновенно)
        await _interactions.RegisterCommandsToGuildAsync(688691719398621214);

        // ⚠ для продакшена — глобально (до часа)
        // await _interactions.RegisterCommandsGloballyAsync();
    }

    private async Task HandleInteractionAsync(SocketInteraction interaction)
    {
        var context = new SocketInteractionContext(_client, interaction);
        await _interactions.ExecuteCommandAsync(context, _services);
    }

    private Task LogAsync(LogMessage msg)
    {
        Console.WriteLine(msg);
        return Task.CompletedTask;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _client.StopAsync();
    }
}
