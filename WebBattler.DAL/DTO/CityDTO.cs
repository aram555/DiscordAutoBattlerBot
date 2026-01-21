namespace WebBattler.DAL.DTO;

public class CityDTO
{
    public string Name { get; set; }
    public int Population { get; set; }
    public int Level { get; set; }
    public List<BuildingDTO> Buildings { get; set; }
}
