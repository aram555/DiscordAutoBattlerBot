using System.Text;

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
                var damage = new Random().Next(15, 50); // Example random damage between 5 and 15

                enemyUnit.TakeDamage(damage); // Example damage value
                sb.AppendLine($"{unit.Name} атакавал {enemyUnit.Name} и нанёс {damage} урон.");
            }
        }

        foreach (var unit in secondArmyUnits)
        {
            var enemyUnit = firstArmyUnits.FirstOrDefault(u => u.IsAlive);

            if (enemyUnit != null)
            {
                var damage = new Random().Next(15, 50); // Example random damage between 5 and 15

                enemyUnit.TakeDamage(damage); // Example damage value
                sb.AppendLine($"{unit.Name} атакавал {enemyUnit.Name} и нанёс {damage} урон.");
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
}
