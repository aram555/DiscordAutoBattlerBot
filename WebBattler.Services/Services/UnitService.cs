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
            ArmyId = _armyRepository.GetIdByName(unitDBO.ArmyName) ?? 0
        };

        _unitRepository.Create(entity);
    }

    public void Delete(string name)
    {
        _unitRepository.Delete(name);
    }

    public void Update(UnitDTO unit)
    {

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
}
