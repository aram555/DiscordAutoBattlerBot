using WebBattler.DAL.Entities;

namespace WebBattler.DAL.Interfaces;

public interface IBuildingSampleRepository
{
    public void Create(BuildingSampleEntity buildingSample);
    public void Update(BuildingSampleEntity buildingSample);
    public void Delete(string buildingSampleName);
    public int GetIdByName(string name);
    public BuildingSampleEntity GetById(int id);
    public List<BuildingSampleEntity> GetAll(ulong ownerId);
    public List<BuildingSampleEntity> GetAllBySessionId(int sessionId);
}
