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

    public void Delete(string countryName)
    {
        if (string.IsNullOrEmpty(countryName))
        {
            return;
        }

        var entity = _context.Countries.FirstOrDefault(c => c.Name == countryName);

        if (entity != null)
        {
            return;
        }

        _context.Countries.Remove(entity);
        _context.SaveChanges();
    }

    public void Update(CountryEntity country)
    {
        _context.Countries.Update(country);
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

    public List<CountryEntity> GetAllBySessionId(int sessionId)
    {
        return _context.Countries
            .Where(c => c.GameSessionId == sessionId)
            .Include(c => c.UnitSamples)
            .Include(c => c.Provinces)
                .ThenInclude(p => p.Neighbours)
            .Include(c => c.Provinces)
                .ThenInclude(p => p.Cities)
                    .ThenInclude(city => city.Buildings)
            .Include(c => c.Armies)
                .ThenInclude(a => a.Units)
            .ToList();
    }

    public Dictionary<int, int> GetIncomebySessionId(int sessionId)
    {
        return _context.Buildings
            .Where(b => b.City.Province.Country.GameSessionId == sessionId)
            .GroupBy(b => b.City.Province.CountryId)
            .Select(g => new { CountryId = g.Key, Income = g.Sum(b => b.Profit) })
            .ToDictionary(x => x.CountryId, x => x.Income);
    }
}
