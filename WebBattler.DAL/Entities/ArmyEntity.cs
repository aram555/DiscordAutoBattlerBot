namespace WebBattler.DAL.Entities;

public class ArmyEntity
{
    public const int MaxTurnCount = 3;

    public int Id { get; set; }
    public ulong OwnerId { get; set; }
    public string Name { get; set; }

    public List<UnitEntity> Units { get; set; }

    public ArmyEntity? Parent { get; set; }
    public int? ParentId { get; set; }
    public List<ArmyEntity> SubArmies { get; set; }

    public CountryEntity Country { get; set; }
    public int CountryId { get; set; }

    public ProvinceEntity Province { get; set; }
    public int ProvinceId { get; set; }

    public CityEntity City { get; set; }
    public int? CityId { get; set; }

    public int CurrentTurnCount { get; set; } = MaxTurnCount;
}
