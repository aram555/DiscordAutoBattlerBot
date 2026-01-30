using WebBattler.DAL.DTO;
using WebBattler.DAL.Models;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Services;

public class ArmyService : IArmyService
{
    private readonly IArmyRepository _repository;
    private readonly ICountryRepository _countryRepository;
    private readonly IProvinceRepository _provinceRepository;

    public ArmyService(IArmyRepository repository, ICountryRepository countryRepository, IProvinceRepository provinceRepository)
    {
        _repository = repository;
        _countryRepository = countryRepository;
        _provinceRepository = provinceRepository;
    }

    public void Create(ArmyDTO army)
    {
        ArmyEntity entity = new ArmyEntity
        {
            OwnerId = army.OwnerId,
            Name = army.Name,
            ParentId = _repository.GetIdByName(army.ParentName),
            CountryId = _countryRepository.GetIdByName(army.CountryName),
            ProvinceId = _provinceRepository.GetIdByName(army.ProvinceName),
            CityId = _provinceRepository.GetIdByName(army?.CityName),
            Units = army.Units.Select(u => new UnitEntity
            {
                Name = u.Name,
                Health = 100f,
                Weapon = u.Weapon,
            }).ToList()
        };

        _repository.Create(entity);
    }

    public void Delete(ArmyDTO army)
    {
        _repository.Delete(new ArmyEntity
        {
            Name = army.Name
        });
    }

    public List<ArmyModel> GetAll(ulong ownerId)
    {
        List<ArmyModel> result = new List<ArmyModel>();

        foreach (var entity in _repository.GetAll(ownerId))
        {
            result.Add(new ArmyModel()
            {
                Name = entity.Name,
                OwnerId = entity.OwnerId,
                Units = entity.Units.Select(u => new UnitModel
                {
                    Name = u.Name,
                    Health = u.Health,
                    Weapon = u.Weapon,
                    OwnerId = u.OwnerId
                }).ToList()
            });
        }

        return result;
    }


    public ArmyModel GetByName(string name)
    {
        var ArmyList = GetAll();

        return ArmyList.FirstOrDefault(a => a.Name == name);
    }
    public ArmyModel GetById(int id)
    {
        var entity = _repository.GetById(id);

        var model = new ArmyModel()
        {
            Name = entity.Name,
            OwnerId = entity.OwnerId,
            SubArmies = entity.SubArmies.Select(s => new ArmyModel()
            {
                Name = s.Name,
                OwnerId = s.OwnerId
            }).ToList(),
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

    public void Update(ArmyDTO army)
    {
        throw new NotImplementedException();
    }
}
