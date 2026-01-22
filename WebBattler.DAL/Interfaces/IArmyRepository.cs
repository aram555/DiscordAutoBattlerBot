using WebBattler.DAL.Entities;
using WebBattler.DAL.Models;

namespace WebBattler.DAL.Interfaces;

public interface IArmyRepository
{
    public void Create(ArmyEntity army);
    public void Update(ArmyEntity army);
    public void Delete(ArmyEntity army);
    public List<ArmyModel> GetAll();
    public int GetIdByName(string name);
}
