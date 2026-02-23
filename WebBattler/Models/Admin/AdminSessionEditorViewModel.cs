using WebBattler.DAL.Models;

namespace WebBattler.Models.Admin;

public class AdminSessionEditorViewModel
{
    public int SessionId { get; set; }
    public string SessionName { get; set; } = string.Empty;

    public IReadOnlyCollection<CountryModel> Countries { get; set; } = Array.Empty<CountryModel>();
    public IReadOnlyCollection<ProvinceModel> Provinces { get; set; } = Array.Empty<ProvinceModel>();
    public IReadOnlyCollection<CityModel> Cities { get; set; } = Array.Empty<CityModel>();
    public IReadOnlyCollection<ArmyModel> Armies { get; set; } = Array.Empty<ArmyModel>();
    public IReadOnlyCollection<UnitModel> Units { get; set; } = Array.Empty<UnitModel>();
    public IReadOnlyCollection<UnitSampleModel> UnitSamples { get; set; } = Array.Empty<UnitSampleModel>();
    public IReadOnlyCollection<BuildingModel> Buildings { get; set; } = Array.Empty<BuildingModel>();
    public IReadOnlyCollection<BuildingSampleModel> BuildingSamples { get; set; } = Array.Empty<BuildingSampleModel>();
}
