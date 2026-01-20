using WebBattler.DAL.DTO;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Models;

namespace WebBattler.DAL.Interfaces;

public interface IUnitRepository
{
    public void Create(UnitEntity unitEntity);
    public List<UnitModel> ShowAll();
    public void Delete(string name);
}
