namespace WebBattler.DAL.DTO;

public class CountryDTO
{
    public ulong OwnerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<ProvinceDTO> Provinces { get; set; }
}
