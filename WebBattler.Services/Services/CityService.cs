using Discord;
using WebBattler.DAL.DTO;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using WebBattler.DAL.Models;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Services;

public class CityService : ICityService
{
    private readonly ICityRepository _repository;
    private readonly IProvinceRepository _provinceRepository;

    public CityService(ICityRepository cityRepository, IProvinceRepository provinceRepository)
    {
        _repository = cityRepository;
        _provinceRepository = provinceRepository;
    }

    public void Create(CityDTO city)
    {
        CityEntity cityEntity = new CityEntity
        {
            Name = city.Name,
            Description = city.Description,
            Population = city.Population,
            Level = city.Level,
            ProvinceId = _provinceRepository.GetIdByName(city.ProvinceName),
            Buildings = city.Buildings?.Select(b => new BuildingEntity
            {
                Name = b.Name,
                Description = b.Description,
                Cost = b.Cost,
                Level = b.Level
            }).ToList() ?? new List<BuildingEntity>()
        };

        _repository.Create(cityEntity);
    }

    public void Delete(CityDTO city)
    {
        _repository.Delete(new CityEntity
        {
            Name = city.Name,
            Population = city.Population,
            Level = city.Level
        });
    }

    public void Update(CityDTO city)
    {
        throw new NotImplementedException();
    }

    public int GetIdByName(string name)
    {
        return _repository.GetIdByName(name);
    }

    public CityModel GetById(int id)
    {
        var entity = _repository.GetById(id);

        return new CityModel()
        {
            Name = entity.Name,
            Description = entity.Description,
            Population = entity.Population,
            Level = entity.Level,
            OwnerId = entity.OwnerId,
            Buildings = entity.Buildings.Select(b => new BuildingModel()
            {
                Name = b.Name,
                Description = b.Description,
                Cost = b.Cost,
                Level = b.Level,
                OwnerId = entity.OwnerId
            }).ToList()
        };
    }

    public List<CityModel> GetAll(ulong ownerId)
    {
        var list = new List<CityModel>();

        foreach(var entity in _repository.GetAll(ownerId))
        {
            list.Add(new CityModel()
            {
                Name = entity.Name,
                Description = entity.Description,
                Population = entity.Population,
                Level = entity.Level,
                OwnerId = entity.OwnerId,
                Buildings = entity.Buildings.Select(b => new BuildingModel()
                {
                    Name = b.Name,
                    Description = b.Description,
                    Cost = b.Cost,
                    Level = b.Level,
                    OwnerId = entity.OwnerId
                }).ToList()
            });
        }

        return list;
    }
}
