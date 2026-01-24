using WebBattler.DAL.Basis;
using WebBattler.DAL.Entities;

namespace WebBattler.Services.Mappers;

public class CityMapper : EntityMapperBase<CityEntity, City>
{
    protected override City MapToDomain(CityEntity entity)
    {

    }

    protected override void MapToEntity(City domain, CityEntity entity)
    {
        throw new NotImplementedException();
    }
}
