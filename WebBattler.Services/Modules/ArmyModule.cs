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

        var armyList = _service.GetAll(Context.User.Id);

        if(!armyList.Any())
        {
            await FollowupAsync("У Вас нет армий, создайте их с помощью команды /create_army");
            return;
        }

        var sb = new StringBuilder();

        foreach (var army in armyList)
        {
            PrintArmy(army, sb, 0);
        }

        EmbedBuilder embed = new EmbedBuilder()
            .WithTitle("Ваша армия")
            .WithColor(Color.DarkGreen)
            .WithDescription($"```text\n{sb}\n```");


        await FollowupAsync(embed: embed.Build());
    }

    void PrintArmy(ArmyModel army, StringBuilder sb, int depth)
    {
        string indent = new string(' ', depth * 3);

        sb.AppendLine($"{indent}▶ {army.Name} (юнитов: {army.Units.Count})");

        if (army.Units.Any())
        {
            foreach (var unit in army.Units)
            {
                sb.AppendLine(
                    $"{indent}   • {unit.Name} | {unit.Weapon} | HP {unit.Health}"
                );
            }
        }
        else
        {
            sb.AppendLine($"{indent}   • Юнитов нет");
        }

        foreach (var subArmy in army.SubArmies)
        {
            PrintArmy(subArmy, sb, depth + 1);
        }

    }
}
