using System.Text;

namespace WebBattler.Services.Army.MoveingService;

public class MoveResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public StringBuilder? BattleResult { get; set; }
    public MoveResult(bool success, string message, StringBuilder? battleResult = null)
    {
        Success = success;
        Message = message;
        BattleResult = battleResult;
    }
}
