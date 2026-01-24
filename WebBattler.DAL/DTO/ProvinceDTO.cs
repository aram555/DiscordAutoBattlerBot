namespace WebBattler.DAL.DTO;

public class ProvinceDTO
{
    public ulong OwnerId { get; set; }
    public string Name { get; set; }
    public List<CityDTO> Cities { get; set; }
}
