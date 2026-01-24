using WebBattler.DAL.Entities;
using WebBattler.DAL.Basis;

namespace WebBattler.Services.Mappers;

public class ArmyMapper : EntityMapperBase<ArmyEntity, DAL.Basis.Army>
{
    protected override DAL.Basis.Army MapToDomain(ArmyEntity entity)
    {
        DAL.Basis.Army army = new();

        return army;
    }

    protected override void MapToEntity(DAL.Basis.Army domain, ArmyEntity entity)
    {
        throw new NotImplementedException();
    }
}
