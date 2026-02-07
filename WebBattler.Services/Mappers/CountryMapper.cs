using WebBattler.DAL.Basis;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Models;

namespace WebBattler.Services.Mappers;

public class CountryMapper : EntityMapperBase<CountryModel, Country>
{
    protected override Country MapToDomain(CountryModel model)
    {
        return new Country(
            model.OwnerId,
            model.Name,
            model.Description);
    }

    protected override void MapToModel(Country domain, CountryModel model)
    {
        model.Name = domain.Name;
        model.Description = domain.Description;
        model.OwnerId = domain.OwnerId;
    }
}
