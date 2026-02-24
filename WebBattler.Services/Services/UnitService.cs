using WebBattler.DAL.DTO;
using WebBattler.DAL.Models;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Services;

public class UnitService : IUnitService
{
    private readonly IUnitRepository _unitRepository;
    private readonly IArmyRepository _armyRepository;
    public UnitService(IUnitRepository unitRepository, IArmyRepository armyRepository)
    {
        _unitRepository = unitRepository;
        _armyRepository = armyRepository;
    }

    public void Create(UnitDTO unitDBO)
    {
        UnitEntity entity = new()
        {
            Name = _GetUniqueName(unitDBO.OwnerId, unitDBO.Name),
            Health = unitDBO.Health,
            Weapon = unitDBO.Weapon,
            Damage = unitDBO.Damage,
            Armor = unitDBO.Armor,
            ArmyId = _armyRepository.GetIdByName(unitDBO.ArmyName) ?? 0,
            OwnerId = unitDBO.OwnerId
        };

        _unitRepository.Create(entity);
    }

    public void Delete(string name)
    {
        _unitRepository.Delete(name);
    }

    public void Update(UnitDTO unit)
    {
        var entity = _unitRepository.GetById(_unitRepository.GetIdByName(unit.OriginalName ?? unit.Name));

        if (!string.IsNullOrWhiteSpace(unit.Name))
        {
            entity.Name = unit.Name;
        }

        if (entity == null)
        {
            return;
        }

        if (unit.Health > 0)
        {
            entity.Health = unit.Health;
        }

        if (!string.IsNullOrWhiteSpace(unit.Weapon))
        {
            entity.Weapon = unit.Weapon;
        }

        if(unit.Damage > 0)
        {
            entity.Damage = unit.Damage;
        }

        if (unit.Armor > 0)
        {
            entity.Armor = unit.Armor;
        }

        if (!string.IsNullOrWhiteSpace(unit.ArmyName))
        {
            entity.ArmyId = _armyRepository.GetIdByName(unit.ArmyName) ?? entity.ArmyId;
        }

        if (unit.OwnerId != default)
        {
            entity.OwnerId = unit.OwnerId;
        }

        _unitRepository.Update(entity);
    }

    public int GetIdByName(string name)
    {
        return _unitRepository.GetIdByName(name);
    }

    public UnitModel GetById(int id)
    {
        var entity = _unitRepository.GetById(id);

        return new UnitModel
        {
            Name = entity.Name,
            Health = entity.Health,
            Weapon = entity.Weapon,
            OwnerId = entity.OwnerId
        };
    }

    public List<UnitModel> GetAll(ulong ownerId)
    {
        var list = new List<UnitModel>();

        foreach(var entity in _unitRepository.GetAll(ownerId))
        {
            list.Add(
                new UnitModel
                {
                    Name = entity.Name,
                    Health = entity.Health,
                    Weapon = entity.Weapon,
                    OwnerId = entity.OwnerId
                });
        }

        return list;
    }

    private string _GetUniqueName(ulong ownerId, string unitName)
    {
        var existingNames = _unitRepository
            .GetAll(ownerId)
            .Where(u => u.Name.StartsWith($"{unitName}-"))
            .Select(u => u.Name)
            .ToHashSet();

        var index = 1;
        var candidate = $"{unitName}-{index}";

        while (existingNames.Contains(candidate))
        {
            index++;
            candidate = $"{unitName}-{index}";
        }

        return candidate;
    }
}
