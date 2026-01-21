using WebBattler.DAL.Entities;
using WebBattler.DAL.Models;

namespace WebBattler.DAL.Interfaces;

public interface IProvinceRepository
{
    public void Create(ProvinceEntity province);
    public void Update(ProvinceEntity city);
    public void Delete(ProvinceEntity city);
    public List<ProvinceModel> GetAll();
}
