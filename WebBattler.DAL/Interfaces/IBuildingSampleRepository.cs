using WebBattler.DAL.Entities;
using WebBattler.DAL.Models;

namespace WebBattler.DAL.Interfaces;

public interface IBuildingSampleRepository
{
    public void Create(BuildingSampleEntity buildingSample);
    public void Update(BuildingSampleEntity buildingSample);
    public void Delete(BuildingSampleEntity buildingSample);
    public List<BuildingSampleModel> GetAll();
    public int GetIdByName(string name);
}
