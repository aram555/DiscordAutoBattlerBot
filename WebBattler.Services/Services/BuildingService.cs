using WebBattler.DAL.DTO;
using WebBattler.DAL.Interfaces;
using WebBattler.DAL.Models;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Services;

public class BuildingService : IBuildingService
{
    private readonly IBuildingRepository _buildingRepository;

    public BuildingService(IBuildingRepository buildingRepository)
    {
        _buildingRepository = buildingRepository;
    }

    public void Create(BuildingDTO building)
    {
        _buildingRepository.Create(new DAL.Entities.BuildingEntity
        {
            Name = building.Name,
            Description = building.Description,
            Cost = building.Cost,
            Level = building.Level
        });
    }

    public void Delete(BuildingDTO building)
    {
        _buildingRepository.Delete(new DAL.Entities.BuildingEntity
        {
            Name = building.Name,
            Description = building.Description,
            Cost = building.Cost,
            Level = building.Level
        });
    }

    public List<BuildingModel> GetAll()
    {
        return _buildingRepository.GetAll();
    }

    public void Update(BuildingDTO building)
    {
        throw new NotImplementedException();
    }
}
