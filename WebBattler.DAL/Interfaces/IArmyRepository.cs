using WebBattler.DAL.Entities;

namespace WebBattler.DAL.Interfaces;

public interface IArmyRepository
{
    public void Create(ArmyEntity army);
    public void Update(ArmyEntity army);
    public void Delete(ArmyEntity army);
    public void MoveToProvince(string armyName, string provinceName);
    public int? GetIdByName(string name);
    public ArmyEntity GetById(int id);
    public List<ArmyEntity> GetAll(ulong ownerId);
}
