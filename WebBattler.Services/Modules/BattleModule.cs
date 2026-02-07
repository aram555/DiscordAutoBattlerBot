using Discord.Interactions;

namespace WebBattler.Services.Modules;

public class BattleModule : InteractionModuleBase<SocketInteractionContext>
{
    public async Task StartBattleAsync(string army1Name, string army2Name)
    {
        await DeferAsync();

        await FollowupAsync($"Битва между {army1Name} и {army2Name} началась!");
    }
}
