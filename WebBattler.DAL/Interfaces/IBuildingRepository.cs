using WebBattler.DAL.Entities;

namespace WebBattler.DAL.Interfaces;

public interface IBuildingRepository
{
    public void Create(BuildingEntity building);
    public void Update(BuildingEntity building);
    public void Delete(string buildingName);
    public int GetIdByName(string name);
    public BuildingEntity GetById(int id);
    public List<BuildingEntity> GetAll(ulong ownerId);
}
