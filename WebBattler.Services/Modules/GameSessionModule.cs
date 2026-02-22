using WebBattler.Services.Interfaces;
using Discord.Interactions;
using WebBattler.DAL.DTO;
using System.Text;

namespace WebBattler.Services.Modules;

public class GameSessionModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly IGameSessionService _service;
    private readonly IProductionOrderService _productionOrderService;
    private readonly IArmyService _armyService;
    private readonly ICountryService _countryService;

    public GameSessionModule(IGameSessionService service,
        IProductionOrderService productionOrderService,
        IArmyService armyService,
        ICountryService countryService)
    {
        _service = service;
        _productionOrderService = productionOrderService;
        _armyService = armyService;
        _countryService = countryService;
    }

    [SlashCommand("create_session", "Создать игровую сессию для текущего сервера")]
    public async Task CreateGameAsync(string name)
    {
        if(Context.Guild == null)
        {
            await RespondAsync("Эту команду можно использовать только на сервере.");
            return;
        }

        var gameSession = _service.Create(new GameSessionDTO
        {
            GuildId = Context.Guild.Id,
            Name = name
        });

        await RespondAsync($"Игровая сессия '{gameSession.Name}' успешно создана для сервера '{Context.Guild.Name}'.");
    }

    [SlashCommand("end_turn", "Завершить текущий ход и начать следующий")]
    public async Task EndTurnAsync()
    {
        await DeferAsync();

        if(Context.Guild == null)
        {
            await FollowupAsync("Эту команду можно использовать только на сервере.");
            return;
        }

        var session = _service.GetByGuildId(Context.Guild.Id);

        if(session == null)
        {
            await FollowupAsync("На этом сервере нет активной игровой сессии.");
            return;
        }

        var response = _service.EndTurn(session.Id);

        foreach (var chank in SplitMessage(response.ToString(), 1900))
        {
            await FollowupAsync(chank);
        }
    }

    private List<string> SplitMessage(string text, int chunkSize)
    {
        var chunks = new List<string>();
        for (int i = 0; i < text.Length; i += chunkSize)
            chunks.Add(text.Substring(i, Math.Min(chunkSize, text.Length - i)));
        return chunks;
    }

}
