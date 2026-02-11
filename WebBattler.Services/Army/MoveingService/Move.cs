using WebBattler.Services.Interfaces;
using WebBattler.DAL.Models;
using WebBattler.Services.Army.BattleService;
using WebBattler.Services.Mappers;
using System.Text;

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

        var enemyArmies = _service.GetAllInProvince(provinceName).Where(a => a.Country.Name != army.Country.Name && a.Name != army.Name).ToList();

        if (enemyArmies.Count > 0)
        {
            StringBuilder battleResults = new StringBuilder();

            foreach (var enemyArmy in enemyArmies)
            {
                var result = _StartBattle(army, enemyArmy);

                battleResults.Append(result.BattleLog.ToString());
            }

            return new MoveResult(true, "Армия успешно перемещена и вступила в бой", battleResults);
        }

        return new MoveResult(true, "Армия успешно перемещена");
    }

    private BattleResult _StartBattle(ArmyModel attacker, ArmyModel defender)
    {
        var mapper = new ArmyMapper();
        var firstArmy = mapper.ToDomain(attacker);
        var secondArmy = mapper.ToDomain(defender);

        return new Battle(firstArmy, secondArmy).StartBattle();
    }

}
