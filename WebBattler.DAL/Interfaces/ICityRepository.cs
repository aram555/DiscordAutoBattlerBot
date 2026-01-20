using WebBattler.DAL.Entities;

namespace WebBattler.DAL.Interfaces;

public interface ICityRepository
{
    public void Create(CityEntity country);
    public void Update(CityEntity country);
    public void Delete(CityEntity country);
    public List<CityEntity> GetAll();
}
