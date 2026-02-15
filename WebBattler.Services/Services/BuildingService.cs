using WebBattler.DAL.DTO;
using WebBattler.DAL.Interfaces;
using WebBattler.DAL.Models;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Services;

public class BuildingService : IBuildingService
{
    private readonly IBuildingRepository _repository;
    private readonly ICityRepository _cityRepository;

    public BuildingService(IBuildingRepository buildingRepository, ICityRepository cityRepository)
    {
        _repository = buildingRepository;
        _cityRepository = cityRepository;
    }

    public void Create(BuildingDTO building)
    {
        var cityId = _cityRepository.GetIdByName(building.CityName);

        _repository.Create(new DAL.Entities.BuildingEntity
        {
            Name = building.Name,
            Description = building.Description,
            Cost = building.Cost,
            Level = building.Level,
            Profit = building.Profit,
            OwnerId = building.OwnerId,
            CityId = cityId
        });
    }

    public void Delete(BuildingDTO building)
    {
        
    }

    public void Update(BuildingDTO building)
    {
        throw new NotImplementedException();
    }

    public int GetIdByName(string name)
    {
        return _repository.GetIdByName(name);
    }

    public BuildingModel GetById(int id)
    {
        var entity = _repository.GetById(id);

        return new BuildingModel()
        {
            Name = entity.Name,
            Description = entity.Description,
            Cost = entity.Cost,
            Level = entity.Level,
            Profit = entity.Profit,
            OwnerId = entity.OwnerId
        };
    }

    public List<BuildingModel> GetAll(ulong ownerId)
    {
        var list = new List<BuildingModel>();

        foreach (var entity in _repository.GetAll(ownerId))
        {
            list.Add(new BuildingModel()
            {
                Name = entity.Name,
                Description = entity.Description,
                Cost = entity.Cost,
                Level = entity.Level,
                Profit = entity.Profit,
                OwnerId = ownerId
            });
        }

        return list;
    }
}
