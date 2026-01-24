using WebBattler.DAL.DTO;
using WebBattler.DAL.Models;

namespace WebBattler.Services.Interfaces;

public interface ICityService
{
    public void Create(CityDTO city);
    public void Update(CityDTO city);
    public void Delete(CityDTO city);
    public List<CityModel> GetAll();
    public int GetIdByName(string name);
}
