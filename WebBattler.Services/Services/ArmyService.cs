using WebBattler.DAL.Basis;
using WebBattler.DAL.DTO;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using WebBattler.DAL.Models;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Services;

public class ArmyService : IArmyService
{
    private readonly IArmyRepository _repository;
    private readonly ICountryRepository _countryRepository;
    private readonly IProvinceRepository _provinceRepository;
    private readonly ICityRepository _cityRepository;

    public ArmyService(IArmyRepository repository, ICountryRepository countryRepository, IProvinceRepository provinceRepository, ICityRepository cityRepository)
    {
        _repository = repository;
        _countryRepository = countryRepository;
        _provinceRepository = provinceRepository;
        _cityRepository = cityRepository;
    }

    public void Create(ArmyDTO army)
    {
        ArmyEntity entity = new ArmyEntity
        {
            OwnerId = army.OwnerId,
            Name = army.Name,
            CountryId = _countryRepository.GetIdByName(army.CountryName),
            ProvinceId = _provinceRepository.GetIdByName(army.ProvinceName),
            Units = army.Units.Select(u => new UnitEntity
            {
                Name = u.Name,
                Health = u.Health,
                Weapon = u.Weapon,
                OwnerId = army.OwnerId,
            }).ToList(),
            SubArmies = new List<ArmyEntity>(),
        };

        if(!string.IsNullOrWhiteSpace(army.ParentName))
        {
            entity.ParentId = _repository.GetIdByName(army.ParentName);
        }

        if(!string.IsNullOrWhiteSpace(army.CityName))
        {
            entity.CityId = _cityRepository.GetIdByName(army?.CityName);
        }

        _repository.Create(entity);
    }

    public void Delete(string armyName)
    {
        _repository.Delete(armyName);
    }

    public void Update(ArmyDTO army)
    {
        throw new NotImplementedException();
    }

    public void MoveToProvince(string armyName, string provinceName)
    {
        _repository.MoveToProvince(armyName, provinceName);
    }

    public int? GetIdByName(string name)
    {
        return _repository.GetIdByName(name);
    }

    public ArmyModel GetById(int id)
    {
        var entity = _repository.GetById(id);

        var model = new ArmyModel()
        {
            Name = entity.Name,
            OwnerId = entity.OwnerId,
            Units = entity.Units.Select(u => new UnitModel()
            {
                Name = u.Name,
                OwnerId = u.OwnerId,
                Weapon = u.Weapon,
                Health = u.Health
            }).ToList()
        };

        return model;
    }

    public List<ArmyModel> GetAll(ulong ownerId)
    {
        var entities = _repository.GetAll(ownerId);

        var models = _GetSubArmies(entities);
        _GetParents(entities, models);

        return models.Values
            .Where(m => entities.First(e => e.Name == m.Name).ParentId == null)
            .ToList();
    }

    public List<ArmyModel> GetAllInProvince(string provinceName)
    {
        var province = _provinceRepository.GetIdByName(provinceName);

        var entities = _repository.GetAllInProvince(province);

        var models = _GetSubArmies(entities);
        _GetParents(entities, models);

        return models.Values
            .Where(m => entities.First(e => e.Name == m.Name).ParentId == null)
            .ToList();
    }

    private Dictionary<int, ArmyModel> _GetSubArmies(List<ArmyEntity> entity)
    {
        var models = entity.ToDictionary(
            e => e.Id,
            e => new ArmyModel()
            {
                Name = e.Name,
                OwnerId = e.OwnerId,
                Province = new ProvinceModel()
                {
                    Name = e.Province.Name,
                    Description = e.Province.Description,
                    OwnerId = e.Province.OwnerId,
                    Neighbours = e.Province.Neighbours.Select(n => new ProvinceModel()
                    {
                        Name = n.Name,
                        Description = n.Description,
                        OwnerId = n.OwnerId,
                    }).ToList()
                },
                Country = new CountryModel()
                {
                    Name = e.Country.Name,
                    Description = e.Country.Description,
                    OwnerId = e.OwnerId,
                },
                Units = e.Units.Select(u => new UnitModel()
                {
                    Name = u.Name,
                    OwnerId = u.OwnerId,
                    Weapon = u.Weapon,
                    Health = u.Health
                }).ToList(),
                SubArmies = new List<ArmyModel>()
            }
        );

        return models;
    }

    private void _GetParents(List<ArmyEntity> entities, Dictionary<int, ArmyModel> models)
    {
        foreach (var entity in entities)
        {
            if (entity.ParentId != null)
            {
                var parent = models[entity.ParentId.Value];
                parent.SubArmies.Add(models[entity.Id]);
            }
        }
    }
}
