using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using WebBattler.DAL.Models;

namespace WebBattler.DAL.Repositories;

public class BuildingSampleRepository : IBuildingSampleRepository
{
    private readonly AutobattlerDbContext _context;

    public BuildingSampleRepository(AutobattlerDbContext context)
    {
        _context = context; 
    }

    public void Create(BuildingSampleEntity buildingSample)
    {
        _context.BuildingSamples.Add(buildingSample);
        _context.SaveChanges();
    }

    public void Delete(BuildingSampleEntity buildingSample)
    {
        var entity = _context.BuildingSamples.FirstOrDefault(b => b.Name == buildingSample.Name);

        if (entity != null)
        {
            _context.BuildingSamples.Remove(buildingSample);
            _context.SaveChanges();
        }
    }

    public List<BuildingSampleModel> GetAll()
    {
        return _context.BuildingSamples.Select(buildingSample => new BuildingSampleModel
        {
            Name = buildingSample.Name,
            Description = buildingSample.Description,
            Cost = buildingSample.Cost,
            Level = buildingSample.Level,
            Country = new CountryModel() { Name = buildingSample.Country.Name }
        }).ToList();
    }

    public int GetIdByName(string name)
    {
        return _context.BuildingSamples.FirstOrDefault(b => b.Name == name).Id;
    }

    public void Update(BuildingSampleEntity buildingSample)
    {
        throw new NotImplementedException();
    }
}
