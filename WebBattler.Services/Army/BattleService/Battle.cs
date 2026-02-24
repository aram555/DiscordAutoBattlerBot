using System.Reactive;
using System.Text;
using WebBattler.DAL.Basis;

namespace WebBattler.Services.Army.BattleService;

public class Battle
{
    private DAL.Basis.Army _firstArmy;
    private DAL.Basis.Army _secondArmy;

    public Battle(DAL.Basis.Army firstArmy, DAL.Basis.Army secondArmy)
    {
        _firstArmy = firstArmy;
        _secondArmy = secondArmy;
    }

    public BattleResult StartBattle()
    {
        var sb = new StringBuilder();

        var firstArmyUnits = _firstArmy.GetAllUnits().Where(u => u.IsAlive);
        var secondArmyUnits = _secondArmy.GetAllUnits().Where(u => u.IsAlive);

        foreach (var unit in firstArmyUnits)
        {
            var enemyUnit = secondArmyUnits.FirstOrDefault(u => u.IsAlive);

            if (enemyUnit != null)
            {
                _BattleStage(unit, enemyUnit, sb);
                _BattleStage(enemyUnit, unit, sb); // Counterattack
            }
        }

        foreach (var unit in secondArmyUnits)
        {
            var enemyUnit = firstArmyUnits.FirstOrDefault(u => u.IsAlive);

            if (enemyUnit != null)
            {
                _BattleStage(unit, enemyUnit, sb);
                _BattleStage(enemyUnit, unit, sb); // Counterattack
            }
        }

        var firstArmyAliveUnits = _firstArmy.GetAllUnits().Count(u => u.IsAlive);
        var secondArmyAliveUnits = _secondArmy.GetAllUnits().Count(u => u.IsAlive);

        sb.AppendLine($"У Армии {_firstArmy.Name} в живых осталось {firstArmyAliveUnits} юнитов");
        sb.AppendLine($"У Армии {_secondArmy.Name} в живых осталось {secondArmyAliveUnits} юнитов");

        return new BattleResult(
            winner: firstArmyAliveUnits > secondArmyAliveUnits ? _firstArmy.Name : _secondArmy.Name,
            battleLog: sb,
            battleName: $"{_firstArmy.Name} vs {_secondArmy.Name}",
            _firstArmy,
            _secondArmy
            );
    }

    private void _BattleStage(DAL.Basis.Unit attacker, DAL.Basis.Unit enemy, StringBuilder sb)
    {
        var damage = enemy.Armor / attacker.Damage;

        enemy.TakeDamage(damage);
        sb.AppendLine($"{attacker.Name} атакавал {attacker.Name} и нанёс {damage} урон.");
    }
}
