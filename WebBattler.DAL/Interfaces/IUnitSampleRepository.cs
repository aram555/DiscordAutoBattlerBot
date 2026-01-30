using WebBattler.DAL.Entities;

namespace WebBattler.DAL.Interfaces;

public interface IUnitSampleRepository
{
    public void Create(UnitSampleEntity unitSample);
    public void Update(UnitSampleEntity unitSample);
    public void Delete(UnitSampleEntity unitSample);
    public int GetIdByName(string name);
    public UnitSampleEntity GetById(int id);
    public List<UnitSampleEntity> GetAll(ulong ownerId);
}
