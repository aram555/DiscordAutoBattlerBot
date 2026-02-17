using Microsoft.EntityFrameworkCore;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using WebBattler.DAL.Models;

namespace WebBattler.DAL.Repositories;

public class UnitSampleRepository : IUnitSampleRepository
{
    private readonly AutobattlerDbContext _dbContext;

    public UnitSampleRepository(AutobattlerDbContext dbContext)
    {
        _dbContext = dbContext; 
    }

    public void Create(UnitSampleEntity unitSample)
    {
        _dbContext.UnitSamples.Add(unitSample);
        _dbContext.SaveChanges();
    }

    public void Delete(string unitSampleName)
    {
        if (string.IsNullOrEmpty(unitSampleName))
        {
            return;
        }

        var entity = _dbContext.UnitSamples.FirstOrDefault(u => u.Name == unitSampleName);

        if (entity != null)
        {
            return;
        }

        _dbContext.UnitSamples.Remove(entity);
        _dbContext.SaveChanges();
    }

    public void Update(UnitSampleEntity unitSample)
    {
        throw new NotImplementedException();
    }

    public int GetIdByName(string name)
    {
        return _dbContext.UnitSamples.FirstOrDefault(u => u.Name == name).Id;
    }

    public UnitSampleEntity GetById(int id)
    {
        return _dbContext.UnitSamples.FirstOrDefault(b => b.Id == id);
    }

    public List<UnitSampleEntity> GetAll(ulong ownerId)
    {
        return _dbContext.UnitSamples
            .Where(a => a.OwnerId == ownerId)
            .ToList();
    }
}
