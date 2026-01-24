namespace WebBattler.DAL.Models;

public class UnitModel
{
    public ulong OwnerId { get; set; }
    public string Name { get; set; }
    public float Health { get; set; }
    public string Weapon { get; set; }
    public ArmyModel Army { get; set; }
}
