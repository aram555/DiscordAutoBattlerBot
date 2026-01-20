using WebBattler.DAL.Entities;

namespace WebBattler.DAL.Interfaces;

public interface ICountryRepository
{
    public void Create(CountryEntity country);
    public void Update(CountryEntity country);
    public void Delete(CountryEntity country);
    public List<CountryEntity> GetAll();
}
