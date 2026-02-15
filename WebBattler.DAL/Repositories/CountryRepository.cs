using Microsoft.EntityFrameworkCore;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using WebBattler.DAL.Models;

namespace WebBattler.DAL.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly AutobattlerDbContext _context;

    public CountryRepository(AutobattlerDbContext context)
    {
        _context = context;
    }

    public void Create(CountryEntity country)
    {
        _context.Countries.Add(country);
        _context.SaveChanges();
    }

    public void Delete(CountryEntity country)
    {
        _context.Countries.Remove(country);
    }

    public void Update(CountryEntity country)
    {
        _context.SaveChanges();
    }

    public CountryEntity GetById(int id)
    {
        return _context.Countries.FirstOrDefault(b => b.Id == id);
    }

    public int GetIdByName(string name)
    {
        return _context.Countries.FirstOrDefault(c => c.Name == name).Id;
    }

    public List<CountryEntity> GetAll(ulong ownerId)
    {
        return _context.Countries
            .Where(a => a.OwnerId == ownerId)
            .Include(c => c.Provinces)
                .ThenInclude(n => n.Neighbours)
            .Include(c => c.Provinces)
                .ThenInclude(c => c.Cities)
                    .ThenInclude(b => b.Buildings)
            .ToList();
    }
}
