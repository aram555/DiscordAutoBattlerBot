using WebBattler.DAL.DTO;
using WebBattler.DAL.Models;

namespace WebBattler.Services.Interfaces;

public interface IArmyService
{
    public void Create(ArmyDTO army);
    public void Update(ArmyDTO army);
    public void Delete(string armyName);
    public bool TryMoveToProvince(string armyName, string provinceName);
    public int? GetIdByName(string name);
    public ArmyModel GetById(int id);
    public List<ArmyModel> GetAll(ulong ownerId);
    public List<ArmyModel> GetAllInProvince(string provinceName);
    public List<ArmyModel> GetAll();
    public void ResetMovementPointsForAllArmies();
    public string ResolveAutomaticBattlesForAllProvinces();
    public string HealSoldiersInAllarmiers(int sessionId);
}
