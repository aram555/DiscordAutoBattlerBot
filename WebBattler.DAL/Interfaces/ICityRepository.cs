using WebBattler.DAL.Entities;
using WebBattler.DAL.Models;

namespace WebBattler.DAL.Interfaces;

public interface ICityRepository
{
    public void Create(CityEntity city);
    public void Update(CityEntity city);
    public void Delete(CityEntity city);
    public List<CityModel> GetAll();
    public int GetIdByName(string name);
}
