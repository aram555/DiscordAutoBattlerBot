namespace WebBattler.DAL.Basis;


public class Building
{
    public ulong OwnerId { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public int Profit { get; set; }

    public Building(ulong ownerId, string name, int level, int profit)
    {
        OwnerId = ownerId; 
        Name = name; 
        Level = level; 
        Profit = profit;
    }
}
