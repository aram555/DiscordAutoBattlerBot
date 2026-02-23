namespace WebBattler.DAL.DTO;

public class ProvinceDTO
{
    public string? OriginalName { get; set; }
    public ulong OwnerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CountryName { get; set; }
    public List<CityDTO> Cities { get; set; }
    public List<ProvinceDTO> Neighbours { get; set; }
}
