using WebBattler.DAL.DTO;
using WebBattler.DAL.Models;

namespace WebBattler.Services.Interfaces;

public interface IProvinceService
{
    public void Create(ProvinceDTO province);
    public void Update(ProvinceDTO province);
    public void Delete(string provinceName);
    public string AddNeightbour(string provinceName, string neightbourName);
    public int GetIdByName(string name);
    public ProvinceModel GetById(int id);
    public List<ProvinceModel> GetAll(ulong ownerId);
    public List<ProvinceModel> GetNeighbours(ulong ownerId);
}
