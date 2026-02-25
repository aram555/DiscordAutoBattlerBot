namespace WebBattler.DAL.Entities;

public class UnitEntity
{
    public int Id { get; set; }
    public ulong OwnerId { get; set; }
    public string Name { get; set; }
    public float MaxHealth { get; set; }
    public float Health { get; set; }
    public string Weapon { get; set; }
    public float Armor { get; set; }
    public float Damage { get; set; }

    public ArmyEntity Army { get; set; }
    public int ArmyId { get; set; }
}
