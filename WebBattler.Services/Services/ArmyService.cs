using WebBattler.DAL.DTO;
using WebBattler.DAL.Models;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Services;

public class ArmyService : IArmyService
{
    private readonly IArmyRepository _repository;

    public ArmyService(IArmyRepository repository)
    {
        _repository = repository;
    }

    public void Create(ArmyDTO army)
    {
        ArmyEntity entity = new ArmyEntity
        {
            Name = army.Name,
            ParentId = _repository.GetIdByName(army.ParentName),
            Units = army.Units.Select(u => new UnitEntity
            {
                Name = u.Name,
                Health = 100f,
                Weapon = u.Weapon,
            }).ToList()
        };
    }

    public void Delete(ArmyDTO army)
    {
        _repository.Delete(new ArmyEntity
        {
            Name = army.Name
        });
    }

    public List<ArmyModel> GetAll()
    {
        return _repository.GetAll();
    }

    public ArmyModel GetByName(string name)
    {
        var ArmyList = _repository.GetAll();

        return ArmyList.FirstOrDefault(a => a.Name == name);
    }

    public void Update(ArmyDTO army)
    {
        throw new NotImplementedException();
    }
}
