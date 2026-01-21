using WebBattler.DAL.Models;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;

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
    }

    public void Delete(ProvinceEntity city)
    {
        _dbContext.Provinces.Remove(city);
    }

    public List<ProvinceModel> GetAll()
    {
        return _dbContext.Provinces.Select(p => new ProvinceModel
        {
            Name = p.Name,
            Cities = p.Cities.Select(city => new CityModel
            {
                Name = city.Name,
                Population = city.Population,
                Level = city.Level
            }).ToList()

        }).ToList();
    }

    public void Update(ProvinceEntity city)
    {
        throw new NotImplementedException();
    }
}
