using WebBattler.DAL.DTO;
using WebBattler.DAL.Models;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using WebBattler.Services.Interfaces;
using System.Text;

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
            Money = country.Money,
            GameSessionId = country.GameSessionId,
            Provinces = new List<ProvinceEntity>(),
            Armies = new List<ArmyEntity>(),
            BuildingSamples = new List<BuildingSampleEntity>(),
            UnitSamples = new List<UnitSampleEntity>(),
        };

        _repository.Create(countryEntity);
    }

    public void Delete(string countryName)
    {
        _repository.Delete(countryName);
    }

    public void Update(CountryDTO country)
    {
        var entity = _repository.GetById(_repository.GetIdByName(country.OriginalName ?? country.Name));
        if (entity == null)
        {
            return;
        }

        if(!string.IsNullOrWhiteSpace(country.Name))
        {
            entity.Name = country.Name;
        }

        if (!string.IsNullOrWhiteSpace(country.Description))
        {
            entity.Description = country.Description;
        }

        if (country.OwnerId != default)
        {
            entity.OwnerId = country.OwnerId;
        }

        if (country.GameSessionId >= 0)
        {
            entity.GameSessionId = country.GameSessionId;
        }

        entity.Money = country.Money;

        _repository.Update(entity);
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
            Money= entity.Money,
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
                Money = entity.Money,
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

    public List<CountryModel> GetAllBySessionId(int sessionId)
    {
        return _repository.GetAllBySessionId(sessionId)
            .Select(entity => new CountryModel
            {
                Name = entity.Name,
                Description = entity.Description,
                Money = entity.Money,
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
                        IsCapital = c.IsCapital,
                        OwnerId = c.OwnerId,
                        Buildings = c.Buildings.Select(b => new BuildingModel()
                        {
                            Name = b.Name,
                            Description = b.Description,
                            Cost = b.Cost,
                            Level = b.Level,
                            Profit = b.Profit,
                            OwnerId = b.OwnerId
                        }).ToList()
                    }).ToList()
                }).ToList()
            })
            .ToList();
    }

    public string ApplyIncomeForTurn(int sessionId)
    {
        var incomeByCountryId = _repository.GetIncomebySessionId(sessionId);
        var countries = _repository.GetAllBySessionId(sessionId);

        var sb = new StringBuilder();

        foreach (var country in countries)
        {
            var income = incomeByCountryId.GetValueOrDefault(country.Id, 0);
            country.Money += income;
            _repository.Update(country);

            sb.AppendLine($"{country.Name}: +{income} монет (всего: {country.Money})");
        }

        return sb.ToString();
    }
}
