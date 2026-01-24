using WebBattler.DAL.Entities;
using WebBattler.DAL.Models;

namespace WebBattler.DAL.Interfaces;

public interface IUnitSampleRepository
{
    public void Create(UnitSampleEntity unitSample);
    public void Update(UnitSampleEntity unitSample);
    public void Delete(UnitSampleEntity unitSample);
    public List<UnitSampleModel> GetAll();
    public int GetIdByName(string name);
}
