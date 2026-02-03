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
            OwnerId = Context.User.Id,
            Name = unitName,
            Weapon = weapon,
            Health = Health,
            Countryname = countryName
        };

        _service.Create(dto);

        await RespondAsync($"Создан шаблон Юнита - {unitName}");
    }

    [SlashCommand("unit_sample_list", "Просмотр доступных шаблонов юнитов.")]
    public async Task UnitSampleListAsync()
    {
        await DeferAsync();

        var list = _service.GetAll(Context.User.Id);

        foreach (var item in list)
        {
            await FollowupAsync(item.Name);
        }
    }

    [SlashCommand("test_unit_sample", "Simple test")]
    public async Task Test()
    {
        await RespondAsync("Test is Completed");
    }
}