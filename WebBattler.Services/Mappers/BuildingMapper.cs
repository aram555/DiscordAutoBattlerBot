using WebBattler.DAL.Basis;
using WebBattler.DAL.Entities;

namespace WebBattler.Services.Mappers;

public class BuildingMapper : EntityMapperBase<BuildingEntity, Building>
{
    protected override Building MapToDomain(BuildingEntity entity)
    {
        return new Building(
            entity.OwnerId,
            entity.Name,
            entity.Level,
            entity.Profit
        );
    }

    protected override void MapToEntity(Building domain, BuildingEntity entity)
    {
        entity.Name = domain.Name;
        entity.Level = domain.Level;
        entity.Profit = domain.Profit;
        entity.Description = domain.Description;
    }
}
