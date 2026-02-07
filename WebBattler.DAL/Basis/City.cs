namespace WebBattler.DAL.Basis;

public class City
{
    public ulong OwnerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Level { get; set; }
    public int Population { get; set; }
    public bool IsCapital { get; set; }

    public Province Province { get; set; }

    public List<Building> Buildings { get; set; }

    public City(ulong ownerId, string name, string desc, int level, int population, bool isCapital)
    {
        OwnerId = ownerId;
        Name = name;
        Description = desc;
        Level = level;
        Population = population;
        IsCapital = isCapital;

        Buildings = new List<Building>();
    }

    public City(ulong ownerId, string name, string description, int level, int population, bool isCapital, List<Building> buildings)
    {
        OwnerId = ownerId;
        Name = name;
        Description = description;
        Level = level;
        Population = population;
        IsCapital = isCapital;
        Buildings = buildings;
    }
}
