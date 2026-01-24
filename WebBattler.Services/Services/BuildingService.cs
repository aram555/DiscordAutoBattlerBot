using WebBattler.DAL.DTO;
using WebBattler.DAL.Interfaces;
using WebBattler.DAL.Models;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Services;

public class BuildingService : IBuildingService
{
    private readonly IBuildingRepository _repository;

    public BuildingService(IBuildingRepository buildingRepository)
    {
        _repository = buildingRepository;
    }

    public void Create(BuildingDTO building)
    {
        _repository.Create(new DAL.Entities.BuildingEntity
        {
            Name = building.Name,
            Description = building.Description,
            Cost = building.Cost,
            Level = building.Level
        });
    }

    public void Delete(BuildingDTO building)
    {
        _repository.Delete(new DAL.Entities.BuildingEntity
        {
            Name = building.Name,
            Description = building.Description,
            Cost = building.Cost,
            Level = building.Level
        });
    }

    public List<BuildingModel> GetAll()
    {
        return _repository.GetAll();
    }

    public int GetIdByName(string name)
    {
        return _repository.GetIdByName(name);
    }

    public void Update(BuildingDTO building)
    {
        throw new NotImplementedException();
    }
}
