using WebBattler.DAL.DTO;
using Discord.Interactions;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Modules;

public class CountryModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly ICountryService _service;

    public CountryModule(ICountryService service)
    {
        _service = service; 
    }

    [SlashCommand("create_country", "Создание страны")]
    public async Task CreateCountryAsync(string name)
    {
        CountryDTO country = new()
        {
            OwnerId = Context.User.Id,
            Name = name,
        };

        _service.Create(country);

        await RespondAsync($"Создана страна{name}");
    }
}
