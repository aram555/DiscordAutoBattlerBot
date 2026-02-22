using WebBattler.DAL.DTO;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using WebBattler.DAL.Models;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Services;

public class BuildingService : IBuildingService
{
    private readonly IBuildingRepository _repository;
    private readonly ICityRepository _cityRepository;
    private readonly ICountryRepository _countryRepository;

    public BuildingService(
        IBuildingRepository buildingRepository,
        ICityRepository cityRepository,
        ICountryRepository countryRepository)
    {
        _repository = buildingRepository;
        _cityRepository = cityRepository;
        _countryRepository = countryRepository;
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

    public void Delete(string buildingName)
    {
        _repository.Delete(buildingName);
    }

    public void Update(BuildingDTO building)
    {
        var entity = _repository.GetById(_repository.GetIdByName(building.Name));

        if (!string.IsNullOrWhiteSpace(building.Description))
        {
            entity.Description = building.Description;
        }
        if (building.Level > 0)
        {
            entity.Level = building.Level;
        }
        if (building.Cost > 0)
        {
            entity.Cost = building.Cost;
        }
        if (building.OwnerId != default)
        {
            entity.OwnerId = building.OwnerId;
        }
        if (!string.IsNullOrWhiteSpace(building.CityName))
        {
            entity.CityId = _cityRepository.GetIdByName(building.CityName);
        }

        entity.Profit = building.Profit;

        _repository.Update(entity);
    }

    public void AddMoney(string buildingName, ulong ownerId)
    {
        var country = _countryRepository.GetAll(ownerId);
        var building = _repository.GetAll(ownerId).FirstOrDefault(b => b.Name == buildingName);

        if (building == null)
        {
            return;
        }
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

    public List<BuildingModel> GetAll()
    {
        var list = new List<BuildingModel>();

        foreach(var entity in _repository.GetAll())
        {
            list.Add(new BuildingModel()
            {
                Name = entity.Name,
                Description = entity.Description,
                Cost = entity.Cost,
                Level = entity.Level,
                Profit = entity.Profit,
                OwnerId = entity.OwnerId
            });
        }

        return list;
    }
}
