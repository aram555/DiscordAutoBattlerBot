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

    public int GetIdByName(string name)
    {
        return _repository.GetIdByName(name);
    }

    public List<BuildingSampleModel> GetAll()
    {
        return _repository.GetAll();
    }

    public void Update(BuildingSampleDTO buildingSample)
    {
        throw new NotImplementedException();
    }
}
