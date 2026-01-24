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

    public void Delete(UnitSampleEntity unitSample)
    {
        var entity = _dbContext.UnitSamples.FirstOrDefault(u => u.Name == unitSample.Name);

        if (entity != null)
        {
            _dbContext.UnitSamples.Remove(entity);
            _dbContext.SaveChanges();
        }
    }

    public List<UnitSampleModel> GetAll()
    {
        return _dbContext.UnitSamples.Select(u => new UnitSampleModel
        {
            Name = u.Name,
            Health = u.Health,
            Country = new CountryModel()
            {
                Name = u.Country.Name
            },
            Weapon = u.Weapon
        }).ToList();
    }

    public int GetIdByName(string name)
    {
        return _dbContext.UnitSamples.FirstOrDefault(u => u.Name == name).Id;
    }

    public void Update(UnitSampleEntity unitSample)
    {
        throw new NotImplementedException();
    }
}
