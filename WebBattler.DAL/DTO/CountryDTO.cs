namespace WebBattler.DAL.DTO;

public class CountryDTO
{
    public ulong OwnerId { get; set; }
    public string Name { get; set; }
    public List<ProvinceDTO> Provinces { get; set; }
}
