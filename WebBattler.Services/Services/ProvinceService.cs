using WebBattler.DAL.DTO;
using WebBattler.DAL.Models;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Services;

public class ProvinceService : IProvinceService
{
    private readonly IProvinceRepository _repository;
    private readonly ICountryRepository _countryRepository;

    public ProvinceService(IProvinceRepository provinceRepository, ICountryRepository countryRepository)
    {
        _repository = provinceRepository;
        _countryRepository = countryRepository;
    }

    public void Create(ProvinceDTO province)
    {
        ProvinceEntity provinceEntity = new ProvinceEntity
        {
            Name = province.Name,
            CountryId = _countryRepository.GetIdByName(province.CountryName),
            Description = province.Description,
            Cities = province.Cities.Select(cityDto => new CityEntity
            {
                Name = cityDto.Name,
                Population = cityDto.Population,
                Buildings = cityDto.Buildings.Select(buildingDto => new BuildingEntity
                {
                    Name = buildingDto.Name,
                    Level = buildingDto.Level
                }).ToList()
            }).ToList()
        };

        _repository.Create(provinceEntity);
    }

    public void Delete(ProvinceDTO province)
    {
        _repository.Delete(new ProvinceEntity
        {
            Name = province.Name
        });
    }

    public void Update(ProvinceDTO province)
    {
        throw new NotImplementedException();
    }

    public int GetIdByName(string name)
    {
        return _repository.GetIdByName(name);
    }

    public ProvinceModel GetById(int id)
    {
        throw new NotImplementedException();
    }

    public List<ProvinceModel> GetAll(ulong ownerId)
    {
        var list = new List<ProvinceModel>();

        foreach(var entity in _repository.GetAll(ownerId))
        {
            list.Add(new ProvinceModel()
            {
                Name = entity.Name,
                Description = entity.Description,
                OwnerId = entity.OwnerId,
                Cities = entity.Cities.Select(c => new CityModel()
                {
                    Name = c.Name,
                    Description = c.Description,
                    Level = c.Level,
                    Population = c.Population,
                    OwnerId = c.OwnerId,
                    Buildings = c.Buildings.Select(c => new BuildingModel()
                    {
                        Name = c.Name,
                        Description = c.Description,
                        Cost = c.Cost,
                        Level = c.Level,
                        OwnerId = c.OwnerId,
                    }).ToList()
                }).ToList()
            });
        }

        return list;
    }
}
