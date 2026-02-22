using WebBattler.DAL.DTO;
using WebBattler.DAL.Models;

namespace WebBattler.Services.Interfaces;

public interface ICountryService
{
    public void Create(CountryDTO country);
    public void Update(CountryDTO country);
    public void Delete(string countryName);
    public int GetIdByName(string name);
    public CountryModel GetById(int id);
    public List<CountryModel> GetAll(ulong ownerId);
    public List<CountryModel> GetAllBySessionId(int sessionId);
    public string ApplyIncomeForTurn(int sessionId);
}
