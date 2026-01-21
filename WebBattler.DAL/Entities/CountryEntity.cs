namespace WebBattler.DAL.Entities;

public class CountryEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public List<ProvinceEntity> Provinces { get; set; }
    public List<ArmyEntity> Armies { get; set; }
}
