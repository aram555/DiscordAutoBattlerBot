using WebBattler.DAL.Entities;

namespace WebBattler.DAL.Interfaces;

public interface ICountryRepository
{
    public void Create(CountryEntity country);
    public void Update(CountryEntity country);
    public void Delete(string countryName);
    public int GetIdByName(string name);
    public CountryEntity GetById(int id);
    public List<CountryEntity> GetAll(ulong ownerId);
    public List<CountryEntity> GetAllBySessionId(int sessionId);
    public Dictionary<int, int> GetIncomebySessionId(int sessionId);
}
