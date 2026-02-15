namespace WebBattler.DAL.Entities;

public class BuildingSampleEntity
{
    public int Id { get; set; }
    public ulong OwnerId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Level { get; set; }
    public int Cost { get; set; }
    public int Profit { get; set; }
    public int BuildTurns { get; set; }

    public CountryEntity Country { get; set; }
    public int CountryId { get; set; }
}
