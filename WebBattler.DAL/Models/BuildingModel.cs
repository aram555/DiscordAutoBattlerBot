namespace WebBattler.DAL.Models;

public class BuildingModel
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Level { get; set; }
    public int Cost { get; set; }
    public CityModel Province { get; set; }
}
