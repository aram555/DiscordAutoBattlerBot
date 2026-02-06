namespace WebBattler.Services.Army.MoveingService;

public class MoveResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public MoveResult(bool success, string message)
    {
        Success = success;
        Message = message;
    }
}
