using Microsoft.EntityFrameworkCore;
using WebBattler.DAL.DTO;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using WebBattler.DAL.Models;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Services;

public class BuildingSampleService : IBuildingSampleService
{
    private readonly IBuildingSampleRepository _repository;

    public BuildingSampleService(IBuildingSampleRepository buildingSampleRepository)
    {
        _repository = buildingSampleRepository;
    }

    public void Create(BuildingSampleDTO buildingSample)
    {
        var entity = new BuildingSampleEntity()
        {
            Name = buildingSample.Name,
            Description = buildingSample.Description,
            Cost = buildingSample.Cost,
            Level = buildingSample.Level,
            CountryId = _repository.GetIdByName(buildingSample.CountryName),
        };

        _repository.Create(entity);
    }

    public void Delete(BuildingSampleDTO buildingSample)
    {
        var entity = new BuildingSampleEntity()
        {
            Name = buildingSample.Name,
        };

        _repository.Delete(entity);
    }

    public void Update(BuildingSampleDTO buildingSample)
    {
        throw new NotImplementedException();
    }

    public int GetIdByName(string name)
    {
        return _repository.GetIdByName(name);
    }

    public BuildingSampleModel GetById(int id)
    {
        var entity = _repository.GetById(id);

        return new BuildingSampleModel()
        {
            Name = entity.Name,
            Description = entity.Description,
            Cost = entity.Cost,
            Level = entity.Level,
            OwnerId = entity.OwnerId
        };
    }

    public List<BuildingSampleModel> GetAll(ulong ownerId)
    {
        var list = new List<BuildingSampleModel>();

        foreach(var entity in _repository.GetAll(ownerId))
        {
            list.Add(new BuildingSampleModel()
            {
                Name = entity.Name,
                Description = entity.Description,
                Cost = entity.Cost,
                Level = entity.Level,
                OwnerId = entity.OwnerId
            });
        }

        return list;
    }

}
