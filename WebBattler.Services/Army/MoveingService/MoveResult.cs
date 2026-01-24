namespace WebBattler.Services.Army.MoveingService;

public class MoveResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public int? TargetProvinceId { get; set; }
    public MoveResult(bool success, string message, int? targetProvinceId = null)
    {
        Success = success;
        Message = message;
        TargetProvinceId = targetProvinceId;
    }
}
