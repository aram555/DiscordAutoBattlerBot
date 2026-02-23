namespace WebBattler.DAL.DTO;

public class UnitDTO
{
    public string? OriginalName { get; set; }
    public ulong OwnerId { get; set; }
    public string Name { get; set; }
    public float Health { get; set; }
    public string Weapon { get; set; }
    public string ArmyName { get; set; }
}
