namespace WebBattler.DAL.Models;

public class CountryModel
{
    public ulong OwnerId { get; set; }
    public string Name { get; set; }
    public List<ProvinceModel> Provinces { get; set; }
}
