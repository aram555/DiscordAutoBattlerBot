namespace WebBattler.DAL.Entities;

public class ArmyEntity
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<UnitEntity> Units { get; set; }

    public ArmyEntity? Parent { get; set; }
    public int ParentId { get; set; }
    public List<ArmyEntity> SubArmies { get; set; }

    public CountryEntity Country { get; set; }
    public int CountryId { get; set; }
}
