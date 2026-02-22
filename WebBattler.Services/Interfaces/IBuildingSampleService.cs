using WebBattler.DAL.DTO;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Models;

namespace WebBattler.Services.Interfaces;

public interface IBuildingSampleService
{
    public void Create(BuildingSampleDTO buildingSample);
    public void Update(BuildingSampleDTO buildingSample);
    public void Delete(string buildingSampleName);
    public int GetIdByName(string name);
    public BuildingSampleModel GetById(int id);
    public List<BuildingSampleModel> GetAll(ulong ownerId);
    public List<BuildingSampleModel> GetAllBySessionId(int sessionId);
}
