namespace WebBattler.DAL.DTO;

public class ArmyDTO
{
    public ulong OwnerId { get; set; }
    public string? OriginalName { get; set; }
    public string Name { get; set; }
    public string Status { get; set; } = string.Empty;
    public int CurrentTurnCount { get; set; }
    public List<UnitDTO> Units { get; set; }
    public string? ParentName { get; set; }
    public string CountryName { get; set; }
    public string ProvinceName { get; set; }
    public string? CityName { get; set; }
}
