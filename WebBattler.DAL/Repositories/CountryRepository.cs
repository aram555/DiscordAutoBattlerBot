using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;

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

    public List<CountryEntity> GetAll()
    {
        return _context.Countries.ToList();
    }

    public void Update(CountryEntity country)
    {
        throw new NotImplementedException();
    }
}
