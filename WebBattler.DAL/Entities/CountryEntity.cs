namespace WebBattler.DAL.Entities;

public class CountryEntity
{
    public int Id { get; set; }
    public ulong OwnerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public List<ProvinceEntity> Provinces { get; set; }
    public List<ArmyEntity> Armies { get; set; }
    public List<UnitSampleEntity> UnitSamples { get; set; }
    public List<BuildingSampleEntity> BuildingSamples { get; set; }
}
