using WebBattler.DAL.DTO;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Models;

namespace WebBattler.Services.Interfaces;

public interface IUnitSampleService
{
    public void Create(UnitSampleDTO unitSample);
    public void Update(UnitSampleDTO unitSample);
    public void Delete(UnitSampleDTO unitSample);
    public List<UnitSampleModel> GetAll();
    public int GetIdByName(string name);
}
