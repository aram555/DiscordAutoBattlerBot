using WebBattler.DAL.Basis;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Models;

namespace WebBattler.Services.Mappers;

public class UnitMapper : EntityMapperBase<UnitModel, Unit>
{
    protected override Unit MapToDomain(UnitModel model)
    {
        return new Unit(
            model.Name,
            model.Health,
            model.Weapon,
            model.Damage,
            model.Armor
        );
    }

    protected override void MapToModel(Unit domain, UnitModel model)
    {
        model.Name = domain.Name;
        model.Health = domain.Health;
        model.Weapon = domain.Weapon;
    }
}
