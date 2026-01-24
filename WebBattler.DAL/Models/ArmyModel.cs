namespace WebBattler.DAL.Models;

public class ArmyModel
{
    public ulong OwnerId { get; set; }
    public string Name { get; set; }
    public CountryModel Country { get; set; }
    public ProvinceModel Province { get; set; }
    public CityModel? City { get; set; }
    public ArmyModel? Parent { get; set; }
    public List<ArmyModel> SubArmies { get; set; }
    public List<UnitModel> Units { get; set; }
}
