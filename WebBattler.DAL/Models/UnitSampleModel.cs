namespace WebBattler.DAL.Models;

public class UnitSampleModel
{
    public ulong OwnerId { get; set; }
    public string Name { get; set; }
    public float Health { get; set; }
    public string Weapon { get; set; }
    public int BuildTurns { get; set; }
    public CountryModel Country { get; set; }
}
