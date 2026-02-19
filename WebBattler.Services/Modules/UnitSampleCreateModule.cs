using Discord;
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
    [DefaultMemberPermissions(GuildPermission.Administrator)]
    public async Task UnitSampleCreateAsync(string unitName, string weapon, int Health, int buildTurns, int cost, string countryName)
    {
        UnitSampleDTO dto = new UnitSampleDTO()
        {
            OwnerId = Context.User.Id,
            Name = unitName,
            Weapon = weapon,
            Health = Health,
            BuildTurns = buildTurns,
            Cost = cost,
            Countryname = countryName
        };

        _service.Create(dto);

        await RespondAsync($"Создан шаблон Юнита - {unitName} ({buildTurns} ходов)");
    }

    [SlashCommand("unit_sample_list", "Просмотр доступных шаблонов юнитов.")]
    public async Task UnitSampleListAsync()
    {
        await DeferAsync();

        var list = _service.GetAll(Context.User.Id);
        var embed = new EmbedBuilder()
            .WithTitle("Список шаблонов юнитов")
            .WithColor(Color.Blue);

        foreach (var item in list)
        {
            embed.AddField($"{item.Name}", $"{item.BuildTurns} ходов | Цена{item.Cost}");
        }

        await FollowupAsync(embed: embed.Build());
    }

    [SlashCommand("test_unit_sample", "Simple test")]
    public async Task Test()
    {
        await RespondAsync("Test is Completed");
    }
}