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
        _dbContext.SaveChanges();
    }

    public void Delete(BuildingEntity building)
    {
        _dbContext.Buildings.Remove(building);
    }

    public void Update(BuildingEntity building)
    {
        throw new NotImplementedException();
    }

    public int GetIdByName(string name)
    {
        return _dbContext.Provinces.FirstOrDefault(b => b.Name == name).Id;
    }

    public BuildingEntity GetById(int id)
    {
        return _dbContext.Buildings.FirstOrDefault(b => b.Id == id);
    }

    public List<BuildingEntity> GetAll(ulong ownerId)
    {
        return _dbContext.Buildings
            .Where(a => a.OwnerId == ownerId)
            .ToList();
    }
}
