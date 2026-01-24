using Discord.Interactions;
using WebBattler.DAL.DTO;
using WebBattler.Services.Interfaces;
using WebBattler.Services.Services;

namespace WebBattler.Services.Modules;

public class UnitSampleCreateModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly IUnitSampleService _service;

    public UnitSampleCreateModule(IUnitSampleService service)
    {
        _service = service; 
    }

    [SlashCommand("unit_sample_create", "Создание шаблона юнита для дальнешйего найма.")]
    public async Task UnitSampleCreateAsync(string unitName, string weapon, int Health, string countryName)
    {
        UnitSampleDTO dto = new UnitSampleDTO()
        {
            Name = unitName,
            Weapon = weapon,
            Health = Health,
            Countryname = countryName
        };

        await RespondAsync("Команда в разработке.");
    }

    [SlashCommand("unit_sample_list", "Просмотр доступных шаблонов юнитов.")]
    public async Task UnitSampleListAsync()
    {
        var list = _service.GetAll();

        foreach (var item in list)
        {
            await RespondAsync(item.Name);
        }
    }

    [SlashCommand("test_unit_sample", "Simple test")]
    public async Task Test()
    {
        await RespondAsync("Test is Completed");
    }
}