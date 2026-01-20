using WebBattler.DAL.DTO;
using WebBattler.DAL.Models;

namespace WebBattler.Services.Interfaces;

public interface IUnitService
{
    public void Create(UnitDTO unitDBO);
    public List<UnitModel> ShowAll();
    public void Delete(string name);
}
