namespace WebBattler.DAL.Basis;

public class Province
{
    public ulong OwnerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public Country Country { get; set; }

    public List<Province> Neighbours { get; set; }

    public List<City> Cities { get; set; }

    public Province(ulong ownerId, string name, string desc)
    {
        OwnerId = ownerId;
        Name = name;
        Description = desc;

        Neighbours = new List<Province>();
        Cities = new List<City>();
    }

    public Province(ulong ownerId, string name, string description, List<Province> neighbours, List<City> cities)
    {
        OwnerId = ownerId;
        Name = name;
        Description = description;
        Neighbours = neighbours;
        Cities = cities;
    }
}
