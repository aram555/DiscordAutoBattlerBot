using Microsoft.EntityFrameworkCore;
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

    public void Delete(string buildingSampleName)
    {
        if (string.IsNullOrEmpty(buildingSampleName))
        {
            return;
        }

        var entity = _context.BuildingSamples.FirstOrDefault(b => b.Name == buildingSampleName);

        if (entity != null)
        {
            return;
        }

        _context.BuildingSamples.Remove(entity);
        _context.SaveChanges();
    }

    public void Update(BuildingSampleEntity buildingSample)
    {
        throw new NotImplementedException();
    }

    public BuildingSampleEntity GetById(int id)
    {
        return _context.BuildingSamples.FirstOrDefault(b => b.Id == id);
    }

    public int GetIdByName(string name)
    {
        return _context.BuildingSamples.FirstOrDefault(a => a.Name == name).Id;
    }

    public List<BuildingSampleEntity> GetAll(ulong ownerId)
    {
        return _context.BuildingSamples
            .Where(a => a.OwnerId == ownerId)
            .ToList();
    }
}
