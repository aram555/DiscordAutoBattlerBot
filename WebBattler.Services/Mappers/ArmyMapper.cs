using WebBattler.DAL.Basis;
using WebBattler.DAL.Models;
using WebBattler.Services.Fabrics;

namespace WebBattler.Services.Mappers;

public class ArmyMapper : EntityMapperBase<ArmyModel, DAL.Basis.Army>
{
    protected override DAL.Basis.Army MapToDomain(ArmyModel model)
    {
        return new ArmyFabric().Build(model);
    }

    protected override void MapToModel(DAL.Basis.Army domain, ArmyModel model)
    {
        model.Name = domain.Name;
        model.OwnerId = domain.Country.OwnerId;
        model.Country = new CountryMapper().ToModel(domain.Country);
        model.Province = new ProvinceMapper().ToModel(domain.Location.Province);
        model.City = domain.Location.City is not null ? new CityMapper().ToModel(domain.Location.City) : null;

    }
}
