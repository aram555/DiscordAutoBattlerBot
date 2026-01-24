using WebBattler.DAL.Entities;
using WebBattler.DAL.Models;

namespace WebBattler.DAL.Interfaces;

public interface IBuildingRepository
{
    public void Create(BuildingEntity building);
    public void Update(BuildingEntity building);
    public void Delete(BuildingEntity building);
    public List<BuildingModel> GetAll();
    public int GetIdByName(string name);
}
