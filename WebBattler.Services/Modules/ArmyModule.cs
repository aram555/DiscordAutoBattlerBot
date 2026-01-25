using WebBattler.DAL.DTO;
using Discord.Interactions;
using WebBattler.Services.Services;
using WebBattler.Services.Interfaces;
using WebBattler.Services.Army.MoveingService;

namespace WebBattler.Services.Modules;

public class ArmyModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly IArmyService _service;

    public ArmyModule(IArmyService service)
    {
        _service = service;
    }

    [SlashCommand("create_army", "Создание армии")]
    public async Task CreateArmyAsync(string name, string parentArmyName, string countryName, string provinceName, string? cityName = null)
    {
        var userId = Context.User.Id;

        ArmyDTO army = new ArmyDTO()
        {
            OwnerId = userId,
            Name = name,
            ParentName = parentArmyName,
            CountryName = countryName,
            ProvinceName = provinceName,
            CityName = cityName,
        };

        _service.Create(army);

        await RespondAsync($"Создана армия {name}");
    }

    [SlashCommand("move_to_province", "Движение к провинции")]
    public async Task MoveToProvinceAsync(string provinceName, string armyName)
    {
        await RespondAsync("In development...");
    }
}
