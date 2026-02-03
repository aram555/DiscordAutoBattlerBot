namespace WebBattler.DAL.DTO;

public class CityDTO
{
    public ulong OwnerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Population { get; set; }
    public int Level { get; set; }
    public string ProvinceName { get; set; }
    public List<BuildingDTO> Buildings { get; set; }
}
