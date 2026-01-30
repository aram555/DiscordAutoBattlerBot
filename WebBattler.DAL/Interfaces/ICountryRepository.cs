using WebBattler.DAL.Entities;

namespace WebBattler.DAL.Interfaces;

public interface ICountryRepository
{
    public void Create(CountryEntity country);
    public void Update(CountryEntity country);
    public void Delete(CountryEntity country);
    public int GetIdByName(string name);
    public CountryEntity GetById(int id);
    public List<CountryEntity> GetAll(ulong ownerId);
}
