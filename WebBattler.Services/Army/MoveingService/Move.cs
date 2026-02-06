using WebBattler.Services.Interfaces;
using WebBattler.DAL.Models;

namespace WebBattler.Services.Army.MoveingService;

public class Move
{
    private readonly IArmyService _service;

    public Move(IArmyService service)
    {
        _service = service;
    }

    public MoveResult MoveToProvince(ArmyModel army, string provinceName)
    {
        var province = army.Province.Neighbours.FirstOrDefault(n => n.Name == provinceName);

        if (province == null)
        {
            return new MoveResult(false, "Провинция не найдена");
        }

        if (army == null)
        {
            return new MoveResult(false, "Армия не найдена");
        }

        var armyProvince = army.Province;

        if (armyProvince == null)
        {
            return new MoveResult(false, "Провинция армии не найдена");
        }

        _service.MoveToProvince(army.Name, provinceName);

        if (army.SubArmies.Count > 0)
        {
            foreach (var subArmy in army.SubArmies)
            {
                _service.MoveToProvince(subArmy.Name, provinceName);
            }
        }

        return new MoveResult(true, "Армия успешно перемещена");
    }
}
