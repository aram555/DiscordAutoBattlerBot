namespace WebBattler.DAL.DTO;

public class ProvinceDTO
{
    public ulong OwnerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CountryName { get; set; }
    public List<CityDTO> Cities { get; set; }
}
