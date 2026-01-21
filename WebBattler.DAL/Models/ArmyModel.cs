namespace WebBattler.DAL.Models;

public class ArmyModel
{
    public string Name { get; set; }
    public CountryModel Country { get; set; }
    public ArmyModel? Parent { get; set; }
    public List<ArmyModel> SubArmies { get; set; }
    public List<UnitModel> Units { get; set; }
}
