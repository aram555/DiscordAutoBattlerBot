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

    public GameSessionModule(IGameSessionService service,
        IProductionOrderService productionOrderService,
        IArmyService armyService)
    {
        _service = service;
        _productionOrderService = productionOrderService;
        _armyService = armyService;
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

        var battleLogs = _armyService.ResolveAutomaticBattlesForAllProvinces();

        _service.AdvanceTurn(session.Id);
        _armyService.ResetMovementPointsForAllArmies();

        var updated = _service.GetById(session.Id);
        var productionLog = _productionOrderService.ProcessTurn(session.Id, updated.CurrentTurn);

        var response = new StringBuilder();
        response.AppendLine($"Новый ход: {updated.CurrentTurn}");

        if(!string.IsNullOrWhiteSpace(battleLogs))
        {
            response.AppendLine($"Битвы в провинциях");
            response.AppendLine(battleLogs);
        }
        if(!string.IsNullOrWhiteSpace(productionLog))
        {
            response.AppendLine($"Производство");
            response.AppendLine(productionLog);
        }

        foreach(var chank in SplitMessage(response.ToString(), 1900))
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
