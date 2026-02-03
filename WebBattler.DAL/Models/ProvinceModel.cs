namespace WebBattler.DAL.Models;

public class ProvinceModel
{
    public ulong OwnerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<CityModel> Cities { get; set; }
}
