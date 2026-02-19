using Discord.Interactions;
using WebBattler.DAL.DTO;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Modules;

public class BuildingModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly IBuildingSampleService _sampleService;
    private readonly IGameSessionService _sessions;
    private readonly IProductionOrderService _orders;
    private readonly ICityService _cities;

    public BuildingModule(
        IBuildingSampleService sampleService,
        IGameSessionService sessions,
        IProductionOrderService orders,
        ICityService cities)
    {
        _sampleService = sampleService;
        _sessions = sessions;
        _orders = orders;
        _cities = cities;
    }

    [SlashCommand("create_building", "Запустить строительство по шаблону")]
    public async Task CreateBuildingAsync(string sampleName, int quantity, string cityName)
    {
        if(string.IsNullOrWhiteSpace(cityName))
        {
            await RespondAsync("Название города не может быть пустым.");
            return;
        }

        if (Context.Guild == null)
        {
            await RespondAsync("Команда доступна только на сервере.");
            return;
        }

        var session = _sessions.GetByGuildId(Context.Guild.Id);
        if (session == null)
        {
            await RespondAsync("Сессия не найдена. Используйте /game_create.");
            return;
        }

        var sample = _sampleService.GetAll(Context.User.Id).FirstOrDefault(s => s.Name == sampleName);
        if (sample == null)
        {
            await RespondAsync("Шаблон строения не найден.");
            return;
        }

        int cityId;
        try
        {
            cityId = _cities.GetIdByName(cityName);
        }
        catch
        {
            await RespondAsync("Город не найден.");
            return;
        }

        var dto = new ProductionOrderDTO
        {
            OwnerId = Context.User.Id,
            GameSessionId = session.Id,
            OrderType = "Building",
            Quantity = quantity,
            Cost = sample.Cost * quantity,
            BuildingSampleId = _sampleService.GetIdByName(sampleName),
            CityId = cityId,
            BuildTurns = sample.BuildTurns
        };

        await RespondAsync(_orders.Queue(dto));
    }
}
