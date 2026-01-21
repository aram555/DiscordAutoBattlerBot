namespace WebBattler.DAL.DTO;

public class BuildingDTO
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Level { get; set; }
    public int Cost { get; set; }
    public string CityName { get; set; }
}
