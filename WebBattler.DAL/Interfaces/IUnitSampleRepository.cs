using WebBattler.DAL.Entities;

namespace WebBattler.DAL.Interfaces;

public interface IUnitSampleRepository
{
    public void Create(UnitSampleEntity unitSample);
    public void Update(UnitSampleEntity unitSample);
    public void Delete(string unitSampleName);
    public int GetIdByName(string name);
    public UnitSampleEntity GetById(int id);
    public List<UnitSampleEntity> GetAll(ulong ownerId);
    public List<UnitSampleEntity> GetAllBySessionId(int sessionId);
}
