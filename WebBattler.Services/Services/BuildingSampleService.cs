using WebBattler.Services.Interfaces;
using WebBattler.DAL.Interfaces;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Models;
using WebBattler.DAL.DTO;

namespace WebBattler.Services.Services;

public class BuildingSampleService : IBuildingSampleService
{
    private readonly IBuildingSampleRepository _repository;
    private readonly ICountryRepository _countryRepository;

    public BuildingSampleService(IBuildingSampleRepository buildingSampleRepository, ICountryRepository countryRepository)
    {
        _repository = buildingSampleRepository;
        _countryRepository = countryRepository;
    }

    public void Create(BuildingSampleDTO buildingSample)
    {
        var entity = new BuildingSampleEntity()
        {
            OwnerId = buildingSample.OwnerId,
            Name = buildingSample.Name,
            Description = buildingSample.Description,
            Cost = buildingSample.Cost,
            Profit = buildingSample.Income,
            Level = buildingSample.Level,
            BuildTurns = buildingSample.BuildTurns,
            CountryId = _countryRepository.GetIdByName(buildingSample.CountryName),
        };

        _repository.Create(entity);
    }

    public void Delete(string buildingSampleName)
    {
        _repository.Delete(buildingSampleName);
    }

    public void Update(BuildingSampleDTO buildingSample)
    {
        var entity = _repository.GetById(_repository.GetIdByName(buildingSample.Name));
        if (entity == null)
        {
            return;
        }

        if (buildingSample.OwnerId != default)
        {
            entity.OwnerId = buildingSample.OwnerId;
        }

        if (!string.IsNullOrWhiteSpace(buildingSample.Description))
        {
            entity.Description = buildingSample.Description;
        }

        if (buildingSample.Level > 0)
        {
            entity.Level = buildingSample.Level;
        }

        if (buildingSample.Cost > 0)
        {
            entity.Cost = buildingSample.Cost;
        }

        if (buildingSample.BuildTurns > 0)
        {
            entity.BuildTurns = buildingSample.BuildTurns;
        }

        if (!string.IsNullOrWhiteSpace(buildingSample.CountryName))
        {
            entity.CountryId = _countryRepository.GetIdByName(buildingSample.CountryName);
        }

        entity.Profit = buildingSample.Income;

        _repository.Update(entity);
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
            BuildTurns = entity.BuildTurns,
            OwnerId = entity.OwnerId
        };
    }

    public List<BuildingSampleModel> GetAll(ulong ownerId)
    {
        var list = new List<BuildingSampleModel>();

        foreach (var entity in _repository.GetAll(ownerId))
        {
            list.Add(new BuildingSampleModel()
            {
                Name = entity.Name,
                Description = entity.Description,
                Cost = entity.Cost,
                Level = entity.Level,
                BuildTurns = entity.BuildTurns,
                OwnerId = entity.OwnerId
            });
        }

        return list;
    }

    public List<BuildingSampleModel> GetAllBySessionId(int sessionId)
    {
        var list = new List<BuildingSampleModel>();

        foreach(var entity in _repository.GetAllBySessionId(sessionId))
        {
            list.Add(new BuildingSampleModel()
            {
                Name = entity.Name,
                Description = entity.Description,
                Cost = entity.Cost,
                Level = entity.Level,
                BuildTurns = entity.BuildTurns,
                OwnerId = entity.OwnerId
            });
        }

        return list;
    }


}
