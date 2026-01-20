using WebBattler.DAL.DTO;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using WebBattler.DAL.Models;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Services;

public class UnitService : IUnitService
{
    private readonly IUnitRepository _unitRepository;
    public UnitService(IUnitRepository unitRepository)
    {
        _unitRepository = unitRepository;
    }

    public void Create(UnitDTO unitDBO)
    {
        UnitEntity entity = new()
        {
            Name = unitDBO.Name,
            Health = 100f,
            Weapon = unitDBO.Weapon,
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
}
