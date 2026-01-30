using WebBattler.DAL.DTO;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Models;

namespace WebBattler.Services.Interfaces;

public interface IBuildingService
{
    public void Create(BuildingDTO building);
    public void Update(BuildingDTO building);
    public void Delete(BuildingDTO building);
    public int GetIdByName(string name);
    public BuildingModel GetById(int id);
    public List<BuildingModel> GetAll(ulong ownerId);
}
