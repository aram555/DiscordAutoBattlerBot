namespace WebBattler.DAL.DTO;

public class ArmyDTO
{
    public string Name { get; set; }
    public List<UnitDTO> Units { get; set; }
    public string? ParentName { get; set; }
    public List<ArmyDTO> SubArmies { get; set; }
    public string CountryName { get; set; }
}
