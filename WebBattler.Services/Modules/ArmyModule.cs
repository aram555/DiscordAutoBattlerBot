using WebBattler.DAL.DTO;
using Discord.Interactions;
using WebBattler.Services.Services;
using WebBattler.Services.Interfaces;
using WebBattler.Services.Army.MoveingService;
using WebBattler.DAL.Models;
using Discord;
using System.Text;
using WebBattler.Services.Fabrics;
using WebBattler.Services.Mappers;

namespace WebBattler.Services.Modules;

public class ArmyModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly IArmyService _service;
    private readonly IProvinceService _provinceService;
    private readonly IUnitService _unitService;

    public ArmyModule(IArmyService service, IProvinceService provinceService, IUnitService unitService)
    {
        _service = service;
        _provinceService = provinceService;
        _unitService = unitService;
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
    public async Task MoveToProvinceAsync(string armyName, string provinceName)
    {
        await DeferAsync();

        var army = _service.GetAll(Context.User.Id).FirstOrDefault(a => a.Name == armyName);

        if (army == null)
        {
            await FollowupAsync("Армия не найдена. Убедитесь, что вы указали правильное имя армии.");
        }

        var result = new Move(_service, _unitService).MoveToProvince(army, provinceName);

        await FollowupAsync(result.Message);
        await FollowupAsync(result.BattleResult?.ToString() ?? "");
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

        sb.AppendLine($"{indent}▶ {army.Name} (юнитов: {army.Units.Count}) | {army.Province.Name} | {army.Country.Name}");

        if (army.Units.Any())
        {
            foreach (var unit in army.Units)
            {
                sb.AppendLine(
                    $"{indent}   • {_GetDisplayUnitName(unit.Name)} | {unit.Weapon} | HP {unit.Health}"
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

    private string _GetDisplayUnitName(string unitName)
    {
        var lastDash = unitName.LastIndexOf('-');

        if (lastDash <= 0)
        {
            return unitName;
        }

        var suffix = unitName[(lastDash + 1)..];

        return int.TryParse(suffix, out _) ? unitName[..lastDash] : unitName;
    }
}
