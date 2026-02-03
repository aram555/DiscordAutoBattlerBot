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

    public void Delete(CityEntity city)
    {
        _context.Cities.Remove(city);
    }
    public void Update(CityEntity city)
    {
        throw new NotImplementedException();
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
