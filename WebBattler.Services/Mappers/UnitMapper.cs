using WebBattler.DAL.Basis;
using WebBattler.DAL.Entities;

namespace WebBattler.Services.Mappers;

public class UnitMapper : EntityMapperBase<UnitEntity, Unit>
{
    protected override Unit MapToDomain(UnitEntity entity)
    {
        return new Unit(
            entity.Name,
            entity.Health,
            entity.Weapon
        );
    }

    protected override void MapToEntity(Unit domain, UnitEntity entity)
    {
        entity.Name = domain.Name;
        entity.Health = domain.Health;
        entity.Weapon = domain.Weapon;
    }
}
