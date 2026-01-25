using WebBattler.DAL.Entities;
using WebBattler.DAL.Basis;

namespace WebBattler.Services.Mappers;

public class ArmyMapper : EntityMapperBase<ArmyEntity, DAL.Basis.Army>
{
    protected override DAL.Basis.Army MapToDomain(ArmyEntity entity)
    {
        DAL.Basis.Army army = new(
            entity.Name,
            new CountryMapper().ToDomain(entity.Country),
            new ArmyLocation() 
            { 
                CountryId = entity.CountryId,
                ProvinceId = entity.ProvinceId,
                CityId = entity.CityId
            },
            new ArmyMapper().ToDomain(entity.Parent)
        );

        return army;
    }

    protected override void MapToEntity(DAL.Basis.Army domain, ArmyEntity entity)
    {
        entity.Name = domain.Name;
        entity.OwnerId = domain.Country.OwnerId;
        entity.ProvinceId = domain.Location.ProvinceId;
        entity.CityId = domain.Location.CityId;
    }
}
