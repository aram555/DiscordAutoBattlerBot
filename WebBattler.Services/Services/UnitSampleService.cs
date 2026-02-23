using WebBattler.DAL.DTO;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using WebBattler.DAL.Models;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Services;

public class UnitSampleService : IUnitSampleService
{
    private readonly IUnitSampleRepository _repository;
    private readonly ICountryRepository _countryRepository;

    public UnitSampleService(IUnitSampleRepository repository, ICountryRepository countryRepository)
    {
        _repository = repository;
        _countryRepository = countryRepository;
    }

    public void Create(UnitSampleDTO unitSample)
    {
        var entity = new UnitSampleEntity()
        {
            OwnerId = unitSample.OwnerId,
            Name = unitSample.Name,
            Health = unitSample.Health,
            Weapon = unitSample.Weapon,
            BuildTurns = unitSample.BuildTurns,
            Cost = unitSample.Cost,
            CountryId = _countryRepository.GetIdByName(unitSample.Countryname)
        };

        _repository.Create(entity);
    }

    public void Delete(string unitSampleName)
    {
        _repository.Delete(unitSampleName);
    }

    public void Update(UnitSampleDTO unitSample)
    {
        var entity = _repository.GetById(_repository.GetIdByName(unitSample.OriginalName ?? unitSample.Name));
        if (entity == null)
        {
            return;
        }

        if(!string.IsNullOrWhiteSpace(unitSample.Name))
        {
            entity.Name = unitSample.Name;
        }

        if (unitSample.OwnerId != default)
        {
            entity.OwnerId = unitSample.OwnerId;
        }

        if (unitSample.Health >= 0)
        {
            entity.Health = unitSample.Health;
        }

        if (!string.IsNullOrWhiteSpace(unitSample.Weapon))
        {
            entity.Weapon = unitSample.Weapon;
        }

        if (unitSample.BuildTurns >= 0)
        {
            entity.BuildTurns = unitSample.BuildTurns;
        }

        if (unitSample.Cost >= 0)
        {
            entity.Cost = unitSample.Cost;
        }

        if (!string.IsNullOrWhiteSpace(unitSample.Countryname))
        {
            entity.CountryId = _countryRepository.GetIdByName(unitSample.Countryname);
        }

        _repository.Update(entity);
    }

    public int GetIdByName(string name)
    {
        return _repository.GetIdByName(name);
    }

    public UnitSampleModel GetById(int id)
    {
        var entity = _repository.GetById(id);

        return new UnitSampleModel()
        {
            Name = entity.Name,
            Health = entity.Health,
            Weapon = entity.Weapon,
            BuildTurns = entity.BuildTurns,
            Cost = entity.Cost,
            OwnerId = entity.OwnerId
        };
    }

    public List<UnitSampleModel> GetAll(ulong ownerId)
    {
        var list = new List<UnitSampleModel>();

        foreach (var entity in _repository.GetAll(ownerId))
        {
            list.Add(new UnitSampleModel()
            {
                Name = entity.Name,
                Health = entity.Health,
                Weapon = entity.Weapon,
                BuildTurns = entity.BuildTurns,
                Cost = entity.Cost,
                OwnerId = entity.OwnerId
            });
        }

        return list;
    }

    public List<UnitSampleModel> GetAllBySessionId(int sessionId)
    {
        var list = new List<UnitSampleModel>();

        foreach (var entity in _repository.GetAllBySessionId(sessionId))
        {
            list.Add(new UnitSampleModel()
            {
                Name = entity.Name,
                Health = entity.Health,
                Weapon = entity.Weapon,
                BuildTurns = entity.BuildTurns,
                Cost = entity.Cost,
                OwnerId = entity.OwnerId,
                Country = entity.Country == null
                    ? new CountryModel { Name = string.Empty, Description = string.Empty, Provinces = new List<ProvinceModel>() }
                    : new CountryModel
                    {
                        Name = entity.Country.Name,
                        Description = entity.Country.Description,
                        OwnerId = entity.Country.OwnerId,
                        Money = entity.Country.Money,
                        Provinces = new List<ProvinceModel>()
                    }
            });
        }

        return list;
    }
}
