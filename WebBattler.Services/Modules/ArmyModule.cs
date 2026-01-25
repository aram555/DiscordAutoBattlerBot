using Discord.Interactions;
using WebBattler.DAL.DTO;
using WebBattler.Services.Services;

namespace WebBattler.Services.Modules;

public class ArmyModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly ArmyService _service;

    public ArmyModule(ArmyService service)
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
            CityName = cityName
        };

        _service.Create(army);

        await RespondAsync("On Development...");
    }
}
