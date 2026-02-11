using System.Text;
using WebBattler.DAL.DTO;
using WebBattler.DAL.Models;
using WebBattler.Services.Army.BattleService;
using WebBattler.Services.Interfaces;
using WebBattler.Services.Mappers;

namespace WebBattler.Services.Army.MoveingService;

public class Move
{
    private readonly IArmyService _service;
    private readonly IUnitService _unitService;

    public Move(IArmyService service, IUnitService unitService)
    {
        _service = service;
        _unitService = unitService;
    }

    public MoveResult MoveToProvince(ArmyModel army, string provinceName)
    {
        var province = army.Province.Neighbours.FirstOrDefault(n => n.Name == provinceName);

        if (army == null)
        {
            return new MoveResult(false, "Армия не найдена");
        }

        if (province == null)
        {
            return new MoveResult(false, "Провинция не найдена");
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

        var battleResult = new Battle(firstArmy, secondArmy).StartBattle();

        _PersistBattleResults(battleResult.FirstArmyResult, attacker.Name);
        _PersistBattleResults(battleResult.SecondArmyResult, defender.Name);

        return battleResult;
    }

    private void _PersistBattleResults(WebBattler.DAL.Basis.Army armyResult, string armyName)
    {
        foreach (var unit in armyResult.GetAllUnits())
        {
            _unitService.Update(new UnitDTO
            {
                Name = unit.Name,
                Health = unit.Health,
                Weapon = unit.Weapon,
                ArmyName = armyName
            });
        }
    }

}
