namespace WebBattler.DAL.DTO;

public class CountryDTO
{
    public ulong OwnerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int GameSessionId { get; set; }
    public int Money { get; set; }
    public List<ProvinceDTO> Provinces { get; set; }
}
