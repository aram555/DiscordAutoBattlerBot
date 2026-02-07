namespace WebBattler.DAL.DTO;

public class BuildingDTO
{
    public ulong OwnerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Level { get; set; }
    public int Cost { get; set; }
    public int Profit { get; set; }
    public string CityName { get; set; }
}
