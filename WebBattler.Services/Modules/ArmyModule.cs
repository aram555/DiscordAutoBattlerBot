using WebBattler.DAL.DTO;
using Discord.Interactions;
using WebBattler.Services.Services;
using WebBattler.Services.Interfaces;
using WebBattler.Services.Army.MoveingService;
using WebBattler.DAL.Models;
using Discord;
using System.Text;

namespace WebBattler.Services.Modules;

public class ArmyModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly IArmyService _service;

    public ArmyModule(IArmyService service)
    {
        _service = service;
    }

    [SlashCommand("create_army", "Создание армии")]
    public async Task CreateArmyAsync(string name, string countryName, string provinceName, string? cityName = null, string? parentArmyName = null)
    {
        await DeferAsync();

        var userId = Context.User.Id;

        ArmyDTO army = new ArmyDTO()
        {
            OwnerId = userId,
            Name = name,
            ParentName = parentArmyName ?? "",
            CountryName = countryName,
            ProvinceName = provinceName,
            CityName = cityName,
            Units = new List<UnitDTO>()
        };

        _service.Create(army);

        await FollowupAsync($"Создана армия {name}");
    }

    [SlashCommand("move_to_province", "Движение к провинции")]
    public async Task MoveToProvinceAsync(string provinceName, string armyName)
    {
        await RespondAsync("In development...");
    }

    [SlashCommand("show_army", "информация о войсках и юнитах")]
    public async Task ShowArmyAsync()
    {
        await DeferAsync();

        EmbedBuilder embed = new EmbedBuilder()
            .WithTitle("Ваша армия");


        var armyList = _service.GetAll(Context.User.Id);

        foreach (var army in armyList)
        {
            PrintArmy(army, embed);
        }

        await FollowupAsync(embed: embed.Build());
    }

    void PrintArmy(ArmyModel army, EmbedBuilder embed)
    {
        var sb = new StringBuilder();

        if (army.Units.Any())
        {
            foreach (var unit in army.Units)
            {
                sb.AppendLine($"{unit.Name} | {unit.Weapon} | HP: {unit.Health}");
            }
        }
        else
        {
            sb.AppendLine("Юнитов нет");
        }

        embed.AddField(
                name: $"{army.Name} | Юниты: {army.Units.Count}",
                value: sb.ToString(),
                inline: false
            );

        foreach (var subArmy in army.SubArmies)
        {
            PrintArmy(subArmy, embed);
        }
    }
}
