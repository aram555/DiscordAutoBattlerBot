namespace WebBattler.DAL.Entities;

public class UnitSampleEntity
{
    public int Id { get; set; }
    public ulong OwnerId { get; set; }
    public string Name { get; set; }
    public float Health { get; set; }
    public string Weapon { get; set; }

    public CountryEntity Country { get; set; }
    public int CountryId { get; set; }
}
