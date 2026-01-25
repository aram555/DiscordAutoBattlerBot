using WebBattler.DAL.Basis;
using WebBattler.DAL.Entities;

namespace WebBattler.Services.Mappers;

public class ProvinceMapper : EntityMapperBase<ProvinceEntity, Province>
{
    protected override Province MapToDomain(ProvinceEntity entity)
    {
        return new Province(
            entity.OwnerId,
            entity.Name,
            entity.Description,
            new CountryMapper().ToDomain(entity.Country));
    }

    protected override void MapToEntity(Province domain, ProvinceEntity entity)
    {
        entity.Name = domain.Name;
        entity.Description = domain.Description;
        entity.OwnerId = domain.OwnerId;
    }
}
