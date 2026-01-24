using Discord.Interactions;

namespace WebBattler.Services.Modules;

public class ArmyModule : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("create_army", "Создание армии")]
    public async Task CreateArmyAsync(string name, string parentArmyName)
    {
        await RespondAsync("On Development...");
    }
}
