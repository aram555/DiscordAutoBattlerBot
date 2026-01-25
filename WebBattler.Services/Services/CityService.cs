using WebBattler.DAL.DTO;
using WebBattler.DAL.Models;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
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

    public List<CityModel> GetAll()
    {
        return _repository.GetAll();
    }

    public int GetIdByName(string name)
    {
        return _repository.GetIdByName(name);
    }

    public void Update(CityDTO city)
    {
        throw new NotImplementedException();
    }
}
