using WebBattler.DAL.DTO;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Models;

namespace WebBattler.Services.Interfaces;

public interface IUnitSampleService
{
    public void Create(UnitSampleDTO unitSample);
    public void Update(UnitSampleDTO unitSample);
    public void Delete(string unitSampleName);
    public int GetIdByName(string name);
    public UnitSampleModel GetById(int id);
    public List<UnitSampleModel> GetAll(ulong ownerId);
    public List<UnitSampleModel> GetAllBySessionId(int sessionId);
}
