namespace WebBattler.DAL.DTO;

public class BuildingSampleDTO
{
    public string? OriginalName { get; set; }
    public ulong OwnerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Level { get; set; }
    public int Cost { get; set; }
    public int Income { get; set; }
    public int BuildTurns { get; set; }
    public string CountryName { get; set; }
}
