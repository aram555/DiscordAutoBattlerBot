using WebBattler.DAL.DTO;
using WebBattler.DAL.Models;

namespace WebBattler.Services.Interfaces;

public interface IUnitService
{
    public void Create(UnitDTO unitDBO);
    public void Delete(string name);
    public void Update(UnitDTO unitDBO);
    public int GetIdByName(string name);
    public UnitModel GetById(int id);
    public List<UnitModel> GetAll(ulong ownerId);
    public List<UnitModel> GetAllBySessionId(int sessionId);
}
