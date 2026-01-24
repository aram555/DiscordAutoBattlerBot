namespace WebBattler.DAL.Basis;

public class Army
{
    public string Name { get; set; }
    public Country Country { get; set; }

    public ArmyLocation Location { get; set; }

    public Army? Parent { get; set; }
    public List<Army> SubArmies { get; set; }

    public List<Unit> Units { get; set; }

    public bool IsLastStep => SubArmies.Count == 0;

    //for testing, will delete soon
    public Army() { }
    public Army(string name, Country country, ArmyLocation location, Army? parent = null)
    {
        Name = name;
        Country = country;
        Location = location;
        Parent = parent;

        SubArmies = new List<Army>();
        Units = new List<Unit>();
    }

    public Army(string name, Country country, ArmyLocation location, Army? parent, List<Army> subArmies, List<Unit> units) : this(name, country, location, parent)
    {
        Name = name;
        Country = country;
        Location = location;
        Parent = parent;
        SubArmies = subArmies;
        Units = units;
    }

    public void ChangeParent(Army newParent)
    {
        Parent?.SubArmies.Remove(this);
        newParent.SubArmies.Add(this);
        Parent = newParent;
    }

    public void AddSubArmy(Army subArmy)
    {
        subArmy.ChangeParent(this);
    }

    public void AddUnit(Unit unit)
    {
        Units.Add(unit);
    }

    public Army FindArmy(string name)
    {
        if (Name.Equals(name, StringComparison.OrdinalIgnoreCase))
        {
            return this;
        }

        foreach (var subUnit in SubArmies)
        {
            var found = subUnit.FindArmy(name);
            if (found != null)
            {
                return found;
            }
        }

        return null;
    }

    public List<Unit> GetAllUnits()
    {
        var result = new List<Unit>();

        result.AddRange(Units);

        foreach(var army in SubArmies)
        {
            result.AddRange(army.GetAllUnits());
        }

        return result;
    }

    public void UpdateLocation(ArmyLocation location, bool updateAll = false)
    {
        Location = location;

        if(updateAll)
        {
            foreach(var army in SubArmies)
            {
                army.UpdateLocation(location, updateAll);
            }
        }
    }
}
