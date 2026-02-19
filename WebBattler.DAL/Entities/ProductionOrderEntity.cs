namespace WebBattler.DAL.Entities;

public class ProductionOrderEntity
{
    public int Id { get; set; }
    public ulong OwnerId { get; set; }

    public int GameSessionId { get; set; }
    public GameSessionEntity GameSession { get; set; } = null!;

    public string OrderType { get; set; } = null!;
    public string Status { get; set; } = "Queued";
    public int Quantity { get; set; }
    public int Cost { get; set; }

    public int? UnitSampleId { get; set; }
    public UnitSampleEntity? UnitSample { get; set; }
    public int? BuildingSampleId { get; set; }
    public BuildingSampleEntity? BuildingSample { get; set; }

    public int? ArmyId { get; set; }
    public ArmyEntity? Army { get; set; }
    public int? CityId { get; set; }
    public CityEntity? City { get; set; }

    public int StartTurn { get; set; }
    public int ReadyTurn { get; set; }
}
