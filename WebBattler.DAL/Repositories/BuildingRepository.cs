using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using WebBattler.DAL.Models;

namespace WebBattler.DAL.Repositories;

public class BuildingRepository : IBuildingRepository
{
    private readonly AutobattlerDbContext _dbContext;

    public BuildingRepository(AutobattlerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Create(BuildingEntity building)
    {
        _dbContext.Buildings.Add(building);
    }

    public void Delete(BuildingEntity building)
    {
        _dbContext.Buildings.Remove(building);
    }

    public List<BuildingModel> GetAll()
    {
        return _dbContext.Buildings.Select(building => new BuildingModel
        {
            Name = building.Name,
            Description = building.Description,
            Cost = building.Cost,
            Level = building.Level
        }).ToList();
    }

    public int GetIdByName(string name)
    {
        return _dbContext.Provinces.FirstOrDefault(b => b.Name == name).Id;
    }

    public void Update(BuildingEntity building)
    {
        throw new NotImplementedException();
    }
}
