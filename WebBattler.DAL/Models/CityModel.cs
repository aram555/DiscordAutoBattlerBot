namespace WebBattler.DAL.Models;

public class CityModel
{
    public string Name { get; set; }
    public int Population { get; set; }
    public int Level { get; set; }
    public List<BuildingModel> Buildings { get; set; }
}
