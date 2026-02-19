namespace WebBattler.DAL.Basis;

public class Country
{
    public ulong OwnerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Money { get; set; }
    public List<Army> Armies { get; set; }
    public List<Province> Provinces { get; set; }

    public Country(ulong ownerId, string name, string desc)
    {
        OwnerId = ownerId;
        Name = name;
        Description = desc;

        Armies = new List<Army>();
        Provinces = new List<Province>();
    }

    public Country(ulong ownerId, string name, string description, int money, List<Army> armies, List<Province> provinces)
    {
        OwnerId = ownerId;
        Name = name;
        Description = description;
        Money = money;
        Armies = armies;
        Provinces = provinces;
    }
}
