using WebBattler.DAL.DTO;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Models;

namespace WebBattler.Services.Interfaces;

public interface IBuildingSampleService
{
    public void Create(BuildingSampleDTO buildingSample);
    public void Update(BuildingSampleDTO buildingSample);
    public void Delete(BuildingSampleDTO buildingSample);
    public List<BuildingSampleModel> GetAll();
    public int GetIdByName(string name);
}
