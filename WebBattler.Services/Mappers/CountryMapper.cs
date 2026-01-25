using WebBattler.DAL.Basis;
using WebBattler.DAL.Entities;

namespace WebBattler.Services.Mappers;

public class CountryMapper : EntityMapperBase<CountryEntity, Country>
{
    protected override Country MapToDomain(CountryEntity entity)
    {
        return new Country(
            entity.OwnerId,
            entity.Name,
            entity.Description);
    }

    protected override void MapToEntity(Country domain, CountryEntity entity)
    {
        entity.Name = domain.Name;
        entity.Description = domain.Description;
        entity.OwnerId = domain.OwnerId;
    }
}
