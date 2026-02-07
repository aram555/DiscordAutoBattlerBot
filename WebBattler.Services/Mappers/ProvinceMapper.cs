using WebBattler.DAL.Basis;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Models;

namespace WebBattler.Services.Mappers;

public class ProvinceMapper : EntityMapperBase<ProvinceModel, Province>
{
    protected override Province MapToDomain(ProvinceModel model)
    {
        return new Province(
            model.OwnerId,
            model.Name,
            model.Description);
    }

    protected override void MapToModel(Province domain, ProvinceModel model)
    {
        model.Name = domain.Name;
        model.Description = domain.Description;
        model.OwnerId = domain.OwnerId;
    }
}
