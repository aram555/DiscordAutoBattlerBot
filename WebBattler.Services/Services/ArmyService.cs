using System.Text;
using WebBattler.DAL.Basis;
using WebBattler.DAL.DTO;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using WebBattler.DAL.Models;
using WebBattler.Services.Army.BattleService;
using WebBattler.Services.Interfaces;
using WebBattler.Services.Mappers;

namespace WebBattler.Services.Services;

public class ArmyService : IArmyService
{
    private readonly IArmyRepository _repository;
    private readonly ICountryRepository _countryRepository;
    private readonly IProvinceRepository _provinceRepository;
    private readonly ICityRepository _cityRepository;
    private readonly IUnitService _unitService;

    public ArmyService(IArmyRepository repository,
        ICountryRepository countryRepository,
        IProvinceRepository provinceRepository,
        ICityRepository cityRepository,
        IUnitService unitServicey)
    {
        _repository = repository;
        _countryRepository = countryRepository;
        _provinceRepository = provinceRepository;
        _cityRepository = cityRepository;
        _unitService = unitServicey;
    }

    public void Create(ArmyDTO army)
    {
        ArmyEntity entity = new ArmyEntity
        {
            OwnerId = army.OwnerId,
            Name = army.Name,
            Status = "Waiting",
            CountryId = _countryRepository.GetIdByName(army.CountryName),
            ProvinceId = _provinceRepository.GetIdByName(army.ProvinceName),
            Units = army.Units.Select(u => new UnitEntity
            {
                Name = u.Name,
                Health = u.Health,
                MaxHealth = u.MaxHealth,
                Weapon = u.Weapon,
                Damage = u.Damage,
                Armor = u.Armor,
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
        var armyId = _repository.GetIdByName(army.OriginalName ?? army.Name);
        if (armyId == null)
        {
            return;
        }

        var entity = _repository.GetById(armyId.Value);
        if (entity == null)
        {
            return;
        }

        if (army.OwnerId != default)
        {
            entity.OwnerId = army.OwnerId;
        }

        if (!string.IsNullOrWhiteSpace(army.Name))
        {
            entity.Name = army.Name;
        }

        if (!string.IsNullOrWhiteSpace(army.Status))
        {
            entity.Status = army.Status;
        }

        if (army.CurrentTurnCount >= 0)
        {
            entity.CurrentTurnCount = army.CurrentTurnCount;
        }


        if (!string.IsNullOrWhiteSpace(army.CountryName))
        {
            entity.CountryId = _countryRepository.GetIdByName(army.CountryName);
        }

        if (!string.IsNullOrWhiteSpace(army.ProvinceName))
        {
            entity.ProvinceId = _provinceRepository.GetIdByName(army.ProvinceName);
        }

        entity.ParentId = string.IsNullOrWhiteSpace(army.ParentName) ? null : _repository.GetIdByName(army.ParentName);
        entity.CityId = string.IsNullOrWhiteSpace(army.CityName) ? null : _cityRepository.GetIdByName(army.CityName);

        _repository.Update(entity);
    }

    public bool TryMoveToProvince(string armyName, string provinceName)
    {
        return _repository.TryMoveToProvince(armyName, provinceName);
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
            Status = entity.Status,
            Units = entity.Units.Select(u => new UnitModel()
            {
                Name = u.Name,
                OwnerId = u.OwnerId,
                Health = u.Health,
                MaxHealth = u.MaxHealth,
                Weapon = u.Weapon,
                Damage = u.Damage,
                Armor = u.Armor
            }).ToList(),
            CurrentTurnCount = entity.CurrentTurnCount
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

    public List<ArmyModel> GetAll()
    {
        var entities = _repository.GetAll();

        var models = _GetSubArmies(entities);
        _GetParents(entities, models);

        return models.Values
            .Where(m => entities.First(e => e.Name == m.Name).ParentId == null)
            .ToList();
    }

    public void ResetMovementPointsForAllArmies()
    {
        _repository.ResetMovementPointsForAllArmies();
    }

    public string ResolveAutomaticBattlesForAllProvinces()
    {
        var armyMapper = new ArmyMapper();

        var allArmies = _repository.GetAll();
        var log = new StringBuilder();

        foreach(var provinceGroup in allArmies.GroupBy(a => a.ProvinceId))
        {
            var armiesInProvince = provinceGroup.ToList();
            var activeArmies = armiesInProvince.Where(a => a.Units.Any(u => u.Health > 0)).ToList();

            if(!activeArmies.Any())
            {
                break;
            }

            var attacker = activeArmies.First();
            var defender = activeArmies.FirstOrDefault(a => a.CountryId != attacker.CountryId);

            if(defender == null)
            {
                break;
            }

            var battleResult = _StartBattle(_ToModel(attacker), _ToModel(defender));

            log.AppendLine($"Провинция {attacker.Province.Name}: {attacker.Name} vs {defender.Name}");
            log.AppendLine(battleResult.BattleLog.ToString());

            armiesInProvince = _repository.GetAllInProvince(attacker.ProvinceId);
            activeArmies = armiesInProvince.Where(a => a.Units.Any(u => u.Health > 0)).ToList();
            if (activeArmies.Count < 2)
            {
                foreach (var army in activeArmies)
                {
                    army.Status = "Waiting";
                    _repository.Update(army);
                }

                TryCaptureProvinceByArmyPresence(attacker.Province.Name);
                break;
            }
        }

        return log.ToString();
    }
    public string HealSoldiersInAllarmiers(int sessionId)
    {
        return _repository.HealSoldiersInAllarmiers(sessionId);
    }

    public bool TryCaptureProvinceByArmyPresence(string provinceName)
    {
        if (string.IsNullOrWhiteSpace(provinceName))
        {
            return false;
        }

        var provinceId = _provinceRepository.GetIdByName(provinceName);
        var armies = _repository.GetAllInProvince(provinceId);
        var aliveArmies = armies.Where(a => a.Units.Any(u => u.Health > 0)).ToList();

        if (!aliveArmies.Any())
        {
            return false;
        }

        var winnerCountryId = aliveArmies.First().CountryId;

        if (aliveArmies.Any(a => a.CountryId != winnerCountryId))
        {
            return false;
        }

        var province = _provinceRepository.GetById(provinceId);

        if (province == null || province.CountryId == winnerCountryId)
        {
            return false;
        }

        var winnerArmy = aliveArmies.First();

        province.CountryId = winnerCountryId;
        province.OwnerId = winnerArmy.OwnerId;

        if (province.Cities != null)
        {
            foreach (var city in province.Cities)
            {
                city.OwnerId = winnerArmy.OwnerId;

                if (city.Buildings == null)
                {
                    continue;
                }

                foreach (var building in city.Buildings)
                {
                    building.OwnerId = winnerArmy.OwnerId;
                }
            }
        }

        _provinceRepository.Update(province);
        return true;
    }

    private ArmyModel _ToModel(ArmyEntity entity)
    {
        return new ArmyModel
        {
            OwnerId = entity.OwnerId,
            Name = entity.Name,
            CurrentTurnCount = entity.CurrentTurnCount,
            Country = new CountryModel
            {
                Name = entity.Country.Name,
                Description = entity.Country.Description,
                OwnerId = entity.Country.OwnerId
            },
            Province = new ProvinceModel
            {
                Name = entity.Province.Name,
                Description = entity.Province.Description,
                OwnerId = entity.Province.OwnerId,
                Neighbours = entity.Province.Neighbours.Select(n => new ProvinceModel
                {
                    Name = n.Name,
                    Description = n.Description,
                    OwnerId = n.OwnerId
                }).ToList()
            },
            Units = entity.Units.Select(u => new UnitModel
            {
                Name = u.Name,
                OwnerId = u.OwnerId,
                Health = u.Health,
                MaxHealth = u.MaxHealth,
                Weapon = u.Weapon,
                Damage = u.Damage,
                Armor = u.Armor
            }).ToList(),
            SubArmies = new List<ArmyModel>()
        };
    }

    private BattleResult _StartBattle(ArmyModel attacker, ArmyModel defender)
    {
        var mapper = new ArmyMapper();
        var firstArmy = mapper.ToDomain(attacker);
        var secondArmy = mapper.ToDomain(defender);

        var battleResult = new Battle(firstArmy, secondArmy).StartBattle();

        _PersistBattleResults(battleResult.FirstArmyResult, attacker.Name);
        _PersistBattleResults(battleResult.SecondArmyResult, defender.Name);

        return battleResult;
    }

    private void _PersistBattleResults(WebBattler.DAL.Basis.Army armyResult, string armyName)
    {
        foreach (var unit in armyResult.GetAllUnits())
        {
            _unitService.Update(new UnitDTO
            {
                Name = unit.Name,
                Health = unit.Health,
                Weapon = unit.Weapon,
                ArmyName = armyName
            });
        }
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
                    MaxHealth = u.MaxHealth,
                    Health = u.Health,
                    Damage = u.Damage,
                    Armor = u.Armor
                }).ToList(),
                SubArmies = new List<ArmyModel>(),
                CurrentTurnCount = e.CurrentTurnCount
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
