using Discord.Interactions;
using WebBattler.DAL.DTO;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Modules;

public class ProvinceModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly IProvinceService _service;

    public ProvinceModule(IProvinceService service)
    {
        _service = service;
    }

    [SlashCommand("create_province", "Создание провинции")]
    public async Task CreateProvinceAsync(string name, string countryName) //надо будет потом убрать эту countryName и сделать метод нахождения активной страны
    {
        ProvinceDTO province = new()
        {
            OwnerId = Context.User.Id,
            Name = name,
            CountryName = countryName
        };

        _service.Create(province);

        await RespondAsync($"Создана провинция {name}");
    }
}
