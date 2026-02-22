using Microsoft.EntityFrameworkCore;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using WebBattler.DAL.Models;

namespace WebBattler.DAL.Repositories;

public class CityRepository : ICityRepository
{
    private readonly AutobattlerDbContext _context;

    public CityRepository(AutobattlerDbContext context)
    {
        _context = context;
    }

    public void Create(CityEntity city)
    {
        _context.Cities.Add(city);
        _context.SaveChanges();
    }

    public void Delete(string cityName)
    {
        if (string.IsNullOrEmpty(cityName))
        {
            return;
        }

        var entity = _context.Cities.FirstOrDefault(c => c.Name == cityName);

        if (entity != null)
        {
            return;
        }

        _context.Cities.Remove(entity);
        _context.SaveChanges();
    }
    public void Update(CityEntity city)
    {
        _context.Cities.Update(city);
        _context.SaveChanges();
    }

    public CityEntity GetById(int id)
    {
        return _context.Cities.FirstOrDefault(b => b.Id == id);
    }

    public int GetIdByName(string name)
    {
        return _context.Cities.FirstOrDefault(c => c.Name == name).Id;
    }

    public List<CityEntity> GetAll(ulong ownerId)
    {
        return _context.Cities
            .Where(a => a.OwnerId == ownerId)
            .Include(c => c.Buildings)
            .ToList();
    }
}
