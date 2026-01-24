using Discord.Interactions;
namespace WebBattler.Services.Modules;

public class TestingModule : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("test", "Replies with pong")]
    public async Task PingAsync()
    {
        await RespondAsync("Pong!");
    }
}
