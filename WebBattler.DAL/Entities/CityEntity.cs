namespace WebBattler.DAL.Entities;

public class CityEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Population {  get; set; }
    public int Level { get; set; }

    public ProvinceEntity Province { get; set; }
    public int ProvinceId { get; set; }

    public List<BuildingEntity> Buildings { get; set; }
}
