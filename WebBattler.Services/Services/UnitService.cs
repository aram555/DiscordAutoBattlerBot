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
            Name = unitDBO.Name,
            Health = 100f,
            Weapon = unitDBO.Weapon,
            ArmyId = _armyRepository.GetIdByName(unitDBO.ArmyName)
        };

        _unitRepository.Create(entity);
    }

    public void Delete(string name)
    {
        _unitRepository.Delete(name);
    }

    public List<UnitModel> ShowAll()
    {
        return _unitRepository.ShowAll();
    }

    public int GetIdByName(string name)
    {
        return _unitRepository.GetIdByName(name);
    }

    public void Update(UnitDTO unit)
    {
        var entity = _unitRepository.ShowAll().FirstOrDefault(u => u.Name == unit.Name);

        if(unit.Name != null)
        {
            entity.Name = unit.Name;
        }

        if(unit.ArmyName != null)
        {
            entity.Army = _armyRepository.GetAll().FirstOrDefault(a => a.Name == unit.ArmyName);
        }

        if(unit.Weapon != null)
        {
            entity.Weapon = unit.Weapon;
        }

        _unitRepository.Update(entity);
    }
}
