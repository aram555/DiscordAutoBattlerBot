namespace WebBattler.DAL.Entities;

public class GameSessionEntity
{
    public int Id { get; set; }
    public ulong GuildId { get; set; }
    public string Name { get; set; } = null!;
    public int CurrentTurn { get; set; }
    public int CurrentYear { get; set; }
    public bool IsActive { get; set; } = true;

    public List<CountryEntity> Countries { get; set; } = new();
    public List<ProductionOrderEntity> ProductionOrders { get; set; } = new();
}
