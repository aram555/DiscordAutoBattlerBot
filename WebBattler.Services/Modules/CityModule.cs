using WebBattler.DAL.DTO;
using Discord.Interactions;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Modules;

public class CityModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly ICityService _service;

    public CityModule(ICityService service)
    {
        _service = service; 
    }

    [SlashCommand("create_city", "создание города")]
    public async Task CreateCityAsync(string name, string provinceName, int level, int population)
    {
        CityDTO cityDTO = new()
        {
            OwnerId = Context.User.Id,
            Name = name,
            ProvinceName = provinceName,
            Level = level,
            Population = population,
        };

        _service.Create(cityDTO);

        await RespondAsync($"Создан город {name}");
    }
}
