using WebBattler.DAL.DTO;
using WebBattler.DAL.Models;

namespace WebBattler.Services.Interfaces;

public interface IArmyService
{
    public void Create(ArmyDTO army);
    public void Update(ArmyDTO army);
    public void Delete(ArmyDTO army);
    public ArmyModel GetByName(string name);
    public ArmyModel GetById(int id);
    public List<ArmyModel> GetAll(ulong ownerId);
}
