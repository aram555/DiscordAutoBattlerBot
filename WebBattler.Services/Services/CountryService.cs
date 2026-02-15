using WebBattler.DAL.DTO;
using WebBattler.DAL.Models;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Services;

public class CountryService : ICountryService
{
    private readonly ICountryRepository _repository;

    public CountryService(ICountryRepository countryRepository)
    {
        _repository = countryRepository;
    }

    public void Create(CountryDTO country)
    {
        CountryEntity countryEntity = new CountryEntity
        {
            Name = country.Name,
            OwnerId = country.OwnerId,
            Description = country.Description,
            GameSessionId = country.GameSessionId,
            Provinces = new List<ProvinceEntity>(),
            Armies = new List<ArmyEntity>(),
            BuildingSamples = new List<BuildingSampleEntity>(),
            UnitSamples = new List<UnitSampleEntity>(),
        };

        _repository.Create(countryEntity);
    }

    public void Delete(CountryDTO country)
    {
        _repository.Delete(new CountryEntity
        {
            Name = country.Name
        });
    }

    public void Update(CountryDTO country)
    {
        throw new NotImplementedException();
    }

    public int GetIdByName(string name)
    {
        return _repository.GetIdByName(name);
    }

    public CountryModel GetById(int id)
    {
        var entity = _repository.GetById(id);

        return new CountryModel
        {
            Name = entity.Name,
            Description = entity.Description,
            OwnerId = entity.OwnerId,
            Provinces = entity.Provinces.Select(p => new ProvinceModel()
            {
                Name = p.Name,
                Description = p.Description,
                OwnerId = p.OwnerId,
                Cities = p.Cities.Select(c => new CityModel()
                {
                    Name = c.Name,
                    Description = c.Description,
                    Population = c.Population,
                    Level = c.Level,
                    OwnerId= c.OwnerId,
                    Buildings = c.Buildings.Select(p => new BuildingModel()
                    {
                        Name = p.Name,
                        Description = p.Description,
                        Cost = p.Cost,
                        Level = p.Level,
                        OwnerId = c.OwnerId
                    }).ToList()
                }).ToList()
            }).ToList()
        };
    }

    public List<CountryModel> GetAll(ulong ownerId)
    {
        var list = new List<CountryModel>();

        foreach(var entity in _repository.GetAll(ownerId))
        {
            list.Add(new CountryModel
            {
                Name = entity.Name,
                Description = entity.Description,
                OwnerId = entity.OwnerId,
                Provinces = entity.Provinces.Select(p => new ProvinceModel()
                {
                    Name = p.Name,
                    Description = p.Description,
                    OwnerId = p.OwnerId,
                    Neighbours = p.Neighbours.Select(n => new ProvinceModel()
                    {
                        Name = n.Name,
                        Description = n.Description,
                        OwnerId = n.OwnerId
                    }).ToList(),
                    Cities = p.Cities.Select(c => new CityModel()
                    {
                        Name = c.Name,
                        Description = c.Description,
                        Population = c.Population,
                        Level = c.Level,
                        OwnerId = c.OwnerId,
                        Buildings = c.Buildings.Select(p => new BuildingModel()
                        {
                            Name = p.Name,
                            Description = p.Description,
                            Cost = p.Cost,
                            Level = p.Level,
                            OwnerId = c.OwnerId
                        }).ToList()
                    }).ToList()
                }).ToList()
            });
        }

        return list;
    }
}
