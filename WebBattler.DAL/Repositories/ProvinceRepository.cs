using WebBattler.DAL.Models;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WebBattler.DAL.Repositories;

public class ProvinceRepository : IProvinceRepository
{
    private readonly AutobattlerDbContext _dbContext;

    public ProvinceRepository(AutobattlerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Create(ProvinceEntity province)
    {
        _dbContext.Provinces.Add(province);
        _dbContext.SaveChanges();
    }

    public void Delete(string provinceName)
    {
        if (string.IsNullOrEmpty(provinceName))
        {
            return;
        }

        var entity = _dbContext.Provinces.FirstOrDefault(p => p.Name == provinceName);

        if (entity != null)
        {
            return;
        }

        _dbContext.Provinces.Remove(entity);
        _dbContext.SaveChanges();
    }

    public void Update(ProvinceEntity province)
    {
        throw new NotImplementedException();
    }

    public void AddNeightbour(string provinceName, string neightbourName)
    {
        var province = _dbContext.Provinces
            .Include(p => p.Neighbours)
            .FirstOrDefault(p => p.Name == provinceName);

        var neighbour = _dbContext.Provinces
            .Include(p => p.Neighbours)
            .FirstOrDefault(p => p.Name == neightbourName);

        if (province == null || neighbour == null || province.Id == neighbour.Id)
        {
            return;
        }

        if(!province.Neighbours.Any(n => n.Id == neighbour.Id))
        {
            province.Neighbours.Add(neighbour);
        }
        if(!neighbour.Neighbours.All(n => n.Id == province.Id))
        {
            neighbour.Neighbours.Add(province);
        }

        _dbContext.SaveChanges();
    }

    public int GetIdByName(string name)
    {
        var province = _dbContext.Provinces
       .FirstOrDefault(p => p.Name == name);

        if (province == null)
        {
            throw new Exception($"Провинция '{name}' не найдена");
        }

        return province.Id;
    }

    public ProvinceEntity GetById(int id)
    {
        return _dbContext.Provinces.FirstOrDefault(b => b.Id == id);
    }

    public List<ProvinceEntity> GetAll(ulong ownerId)
    {
        return _dbContext.Provinces
            .Where(a => a.OwnerId == ownerId)
            .Include(a => a.Neighbours)
            .Include(p => p.Cities)
            .ThenInclude(c => c.Buildings)
            .ToList();
    }

    public List<ProvinceEntity> GetNeighbours(ulong ownerId)
    {
        return _dbContext.Provinces
            .Where(p => p.OwnerId == ownerId)
            .ToList();
    }
}
