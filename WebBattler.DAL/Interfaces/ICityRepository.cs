using WebBattler.DAL.Entities;

namespace WebBattler.DAL.Interfaces;

public interface ICityRepository
{
    public void Create(CityEntity city);
    public void Update(CityEntity city);
    public void Delete(string cityName);
    public int GetIdByName(string name);
    public CityEntity GetById(int id);
    public List<CityEntity> GetAll(ulong ownerId);
}
