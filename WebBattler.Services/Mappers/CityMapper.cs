using WebBattler.DAL.Basis;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Models;

namespace WebBattler.Services.Mappers;

public class CityMapper : EntityMapperBase<CityModel, City>
{
    protected override City MapToDomain(CityModel model)
    {
        City city = new City(
            model.OwnerId,
            model.Name,
            model.Description,
            model.Level,
            model.Population,
            model.IsCapital);

        return city;
    }

    protected override void MapToModel(City domain, CityModel model)
    {
        model.OwnerId = domain.OwnerId;
        model.Name = domain.Name;
        model.Description = domain.Description;
        model.Level = domain.Level;
        model.Population = domain.Population;
        model.IsCapital = domain.IsCapital;
    }
}
