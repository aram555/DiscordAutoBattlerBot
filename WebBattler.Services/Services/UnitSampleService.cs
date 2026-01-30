using WebBattler.DAL.DTO;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using WebBattler.DAL.Models;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Services;

public class UnitSampleService : IUnitSampleService
{
    private readonly IUnitSampleRepository _repository;

    public UnitSampleService(IUnitSampleRepository repository)
    {
        _repository = repository; 
    }

    public void Create(UnitSampleDTO unitSample)
    {
        var entity = new UnitSampleEntity()
        {
            Name = unitSample.Name,
            Health = unitSample.Health,
            Weapon = unitSample.Weapon,
            CountryId = GetIdByName(unitSample.Name)
        };

        _repository.Create(entity);
    }

    public void Delete(UnitSampleDTO unitSample)
    {
        _repository.Delete(new UnitSampleEntity()
        {
            Name = unitSample.Name
        });
    }

    public void Update(UnitSampleDTO unitSample)
    {
        throw new NotImplementedException();
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
                OwnerId = entity.OwnerId
            });
        }

        return list;
    }
}
