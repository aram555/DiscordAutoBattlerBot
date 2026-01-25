using WebBattler.DAL.Basis;
using WebBattler.DAL.Entities;

namespace WebBattler.Services.Mappers;

public class CityMapper : EntityMapperBase<CityEntity, City>
{
    protected override City MapToDomain(CityEntity entity)
    {
        City city = new City(
            entity.OwnerId,
            entity.Name,
            entity.Description,
            entity.Level,
            entity.Population,
            entity.IsCapital,
            new ProvinceMapper().ToDomain(entity.Province));

        return city;
    }

    protected override void MapToEntity(City domain, CityEntity entity)
    {
        entity.OwnerId = domain.OwnerId;
        entity.Name = domain.Name;
        entity.Description = domain.Description;
        entity.Level = domain.Level;
        entity.Population = domain.Population;
        entity.IsCapital = domain.IsCapital;
    }
}
