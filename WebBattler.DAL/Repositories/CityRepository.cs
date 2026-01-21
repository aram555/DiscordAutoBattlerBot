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
    }

    public void Delete(CityEntity city)
    {
        _context.Cities.Remove(city);
    }

    public List<CityModel> GetAll()
    {
        return _context.Cities.Select(city => new CityModel
        {
            Name = city.Name,
            Population = city.Population,
            Level = city.Level
        }).ToList();
    }

    public void Update(CityEntity city)
    {
        throw new NotImplementedException();
    }
}
