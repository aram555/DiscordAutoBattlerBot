namespace WebBattler.DAL.Models;

public class BuildingSampleModel
{
    public ulong OwnerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Level { get; set; }
    public int Cost { get; set; }
    public int Profit { get; set; }
    public int BuildTurns { get; set; }
    public CountryModel Country { get; set; }
}
