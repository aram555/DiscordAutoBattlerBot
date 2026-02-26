using Discord.Interactions;
using WebBattler.Services.Interfaces;
using WebBattler.DAL.DTO;
using Discord;

namespace WebBattler.Services.Modules;

public class BuildingSampleModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly IBuildingSampleService _service;

    public BuildingSampleModule(IBuildingSampleService service)
    {
        _service = service;
    }

    [SlashCommand("building_sample_create", "Создание шаблона строения")]
    //[DefaultMemberPermissions(GuildPermission.Administrator)]
    public async Task BuildingSampleCreateAsync(string name, string desc, int level, int cost, int income, int buildTurns, string countryName)
    {
        _service.Create(new BuildingSampleDTO
        {
            OwnerId = Context.User.Id,
            Name = name,
            Description = desc,
            Level = level,
            Cost = cost,
            Income = income,
            BuildTurns = buildTurns,
            CountryName = countryName
        });

        await RespondAsync($"Создан шаблон строения {name} ({buildTurns} ходов)");
    }

    [SlashCommand("building_sample_list", "Список шаблонов строений")]
    public async Task BuildingSampleListAsync()
    {
        await DeferAsync();

        foreach (var item in _service.GetAll(Context.User.Id))
        {
            await FollowupAsync($"{item.Name} | {item.BuildTurns} ходов");
        }
    }
}
