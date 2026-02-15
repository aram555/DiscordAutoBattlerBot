using WebBattler.Services.Interfaces;
using Discord.Interactions;
using WebBattler.DAL.DTO;

namespace WebBattler.Services.Modules;

public class GameSessionModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly IGameSessionService _service;
    private readonly IProductionOrderService _productionOrderService;

    public GameSessionModule(IGameSessionService service, IProductionOrderService productionOrderService)
    {
        _service = service;
        _productionOrderService = productionOrderService;
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
        if(Context.Guild == null)
        {
            await RespondAsync("Эту команду можно использовать только на сервере.");
            return;
        }

        var session = _service.GetByGuildId(Context.Guild.Id);

        if(session == null)
        {
            await RespondAsync("На этом сервере нет активной игровой сессии.");
            return;
        }

        _service.AdvanceTurn(session.Id);

        var updated = _service.GetById(session.Id);
        var log = _productionOrderService.ProcessTurn(session.Id, updated.CurrentTurn);

        await RespondAsync(string.IsNullOrWhiteSpace(log)
            ? $"Новый ход: {updated.CurrentTurn}. Очереди пока не завершены."
            : $"Новый ход: {updated.CurrentTurn}\n{log}");
    }
}
