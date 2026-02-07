using WebBattler.DAL.Basis;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Models;

namespace WebBattler.Services.Mappers;

public class BuildingMapper : EntityMapperBase<BuildingModel, Building>
{
    protected override Building MapToDomain(BuildingModel model)
    {
        return new Building(
            model.OwnerId,
            model.Name,
            model.Level,
            model.Profit
        );
    }

    protected override void MapToModel(Building domain, BuildingModel model)
    {
        model.Name = domain.Name;
        model.Level = domain.Level;
        model.Profit = domain.Profit;
        model.Description = domain.Description;
    }
}
