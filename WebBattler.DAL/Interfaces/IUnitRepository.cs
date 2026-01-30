using WebBattler.DAL.Entities;

namespace WebBattler.DAL.Interfaces;

public interface IUnitRepository
{
    public void Create(UnitEntity unitEntity);
    public void Delete(string name);
    public void Update(UnitEntity unitEntity);
    public int GetIdByName(string name);
    public UnitEntity GetById(int id);
    public List<UnitEntity> GetAll(ulong ownerId);
}
