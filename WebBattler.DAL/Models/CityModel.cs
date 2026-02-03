namespace WebBattler.DAL.Models;

public class CityModel
{
    public ulong OwnerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Population { get; set; }
    public int Level { get; set; }
    public List<BuildingModel> Buildings { get; set; }
}
