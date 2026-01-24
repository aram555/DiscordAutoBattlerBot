namespace WebBattler.DAL.Entities;

public class BuildingEntity
{
    public int Id { get; set; }
    public ulong OwnerId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Level { get; set; }
    public int Cost { get; set; }

    public CityEntity City { get; set; }
    public int CityId { get; set; }
}
