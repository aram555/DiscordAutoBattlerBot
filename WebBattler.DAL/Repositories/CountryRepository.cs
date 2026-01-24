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
    }

    public void Delete(CountryEntity country)
    {
        _context.Countries.Remove(country);
    }

    public List<CountryModel> GetAll()
    {
        return _context.Countries.Select(c => new CountryModel
        {
            Name = c.Name,
            Provinces = c.Provinces.Select(p => new ProvinceModel
            {
                Name = p.Name,
                Cities = p.Cities.Select(city => new CityModel
                {
                    Name = city.Name,
                    Population = city.Population,
                    Level = city.Level
                }).ToList()

            }).ToList()

        }).ToList();
    }

    public int GetIdByName(string name)
    {
        return _context.Countries.FirstOrDefault(c => c.Name == name).Id;
    }

    public void Update(CountryEntity country)
    {
        throw new NotImplementedException();
    }
}
