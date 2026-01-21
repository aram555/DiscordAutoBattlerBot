using WebBattler.DAL.Models;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;

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
            Level = city.Level,
            Buildings = city.Buildings.Select(building => new BuildingModel
            {
                Name = building.Name,
                Description = building.Description,
                Cost = building.Cost,
                Level = building.Level
            }).ToList()
        }).ToList();
    }

    public void Update(CityEntity city)
    {
        throw new NotImplementedException();
    }
}
