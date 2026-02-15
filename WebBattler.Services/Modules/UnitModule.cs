using WebBattler.DAL.DTO;
using Discord.Interactions;
using WebBattler.DAL.Entities;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Modules;

public class UnitModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly IUnitSampleService _sampleService;
    private readonly IGameSessionService _gameSessionService;
    private readonly IProductionOrderService _productionOrderService;
    private readonly IArmyService _armyService;

    public UnitModule(IUnitSampleService sampleService, IGameSessionService gameSessionService, IProductionOrderService productionOrderService, IArmyService armyService)
    {
        _sampleService = sampleService;
        _gameSessionService = gameSessionService;
        _productionOrderService = productionOrderService;
        _armyService = armyService;
    }

    [SlashCommand("create_unit", "Начать подготовку юнитов")]
    public async Task CreateUnitAsync(string sampleName, int quantity, string armyName)
    {
        if (Context.Guild == null)
        {
            await RespondAsync("Команда доступна только на сервере.");
            return;
        }

        var session = _gameSessionService.GetByGuildId(Context.Guild.Id);
        if(session == null)
        {
            await RespondAsync("На этом сервере нет активной игровой сессии. Попросите администраторов создать её");
        }

        var sample = _sampleService.GetAll(Context.User.Id).FirstOrDefault(s => s.Name == sampleName);
        if(sample == null)
        {
            await RespondAsync("Шаблон юнита не найден.");
            return;
        }

        _productionOrderService.Queue(new ProductionOrderDTO
        {
            OwnerId = Context.User.Id,
            GameSessionId = session.Id,
            OrderType = "Unit",
            Quantity = quantity,
            UnitSampleId = _sampleService.GetIdByName(sampleName),
            ArmyId = _armyService.GetIdByName(armyName),
            BuildTurns = sample.BuildTurns
        });

        await RespondAsync($"Подготовка {sampleName} x{quantity} начата. Готово через {sample.BuildTurns} ход(а/ов).");
    }
}
