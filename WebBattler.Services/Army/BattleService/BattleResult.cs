using System.Text;

namespace WebBattler.Services.Army.BattleService;

public class BattleResult
{
    public string Winner { get; set; }
    public StringBuilder BattleLog { get; set; }
    public string BattleName { get; set; }

    public DAL.Basis.Army FirstArmyResult { get; set; }
    public DAL.Basis.Army SecondArmyResult { get; set; }

    public BattleResult(string winner, StringBuilder battleLog, string battleName, DAL.Basis.Army firstArmyResult, DAL.Basis.Army secondArmyResult)
    {
        Winner = winner;
        BattleLog = battleLog;
        BattleName = battleName;
        FirstArmyResult = firstArmyResult;
        SecondArmyResult = secondArmyResult;
    }
}
