using WebBattler.DAL.DTO;
using WebBattler.DAL.Models;

namespace WebBattler.Services.Interfaces;

public interface ICountryService
{
    public void Create(CountryDTO country);
    public void Update(CountryDTO country);
    public void Delete(CountryDTO country);
    public List<CountryModel> GetAll();
}
