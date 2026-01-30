using WebBattler.DAL.DTO;
using Discord.Interactions;
using WebBattler.DAL.Entities;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Modules;

public class UnitModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly IUnitService _service;
    private readonly IUnitSampleService _sampleService;

    public UnitModule(IUnitService service, IUnitSampleService unitSampleService)
    {
        _service = service;
        _sampleService = unitSampleService;
    }

    [SlashCommand("create_unit", "Создание юнитвоя для армии")]
    public async Task CreateUnitAsync(string sampleName, int quantity, string armyName)
    {
        var sample = _sampleService.GetAll(Context.User.Id).FirstOrDefault(s => s.Name == sampleName);

        UnitDTO unitDTO = new()
        {
            OwnerId = Context.User.Id,
            Name = sample.Name,
            Weapon = sample.Weapon,
            ArmyName = armyName
        };

        for(int i = 0; i < quantity; i++)
        {
            _service.Create(unitDTO);
        }

        await RespondAsync($"Создан {sampleName} {quantity} раз для армии {armyName}");
    }
}
