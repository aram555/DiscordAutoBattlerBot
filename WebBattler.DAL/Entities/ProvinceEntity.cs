namespace WebBattler.DAL.Entities;

public class ProvinceEntity
{
    public int Id { get; set; }
    public string Name { get; set; }

    public CountryEntity Country { get; set; }
    public int CountryId { get; set; }

    public List<CityEntity> Cities { get; set; }
}
