using WebBattler.DAL.Models;

namespace WebBattler.Models.Admin;

public class SessionManagementViewModel
{
    public int SessionId { get; set; }
    public string SessionName { get; set; } = string.Empty;

    public IReadOnlyCollection<CountryModel> Countries { get; set; } = Array.Empty<CountryModel>();
    public IReadOnlyCollection<ProvinceModel> Provinces { get; set; } = Array.Empty<ProvinceModel>();
    public IReadOnlyCollection<CityModel> Cities { get; set; } = Array.Empty<CityModel>();
    public IReadOnlyCollection<ArmyModel> Armies { get; set; } = Array.Empty<ArmyModel>();
}
