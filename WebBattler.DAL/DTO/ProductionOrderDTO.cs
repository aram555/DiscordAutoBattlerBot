namespace WebBattler.DAL.DTO;

public class ProductionOrderDTO
{
    public ulong OwnerId { get; set; }
    public int GameSessionId { get; set; }
    public string OrderType { get; set; } = null!;
    public int Quantity { get; set; }
    public int? UnitSampleId { get; set; }
    public int? BuildingSampleId { get; set; }
    public int? ArmyId { get; set; }
    public int? CityId { get; set; }
    public int BuildTurns { get; set; }
}
