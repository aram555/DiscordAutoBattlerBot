using WebBattler.DAL.DTO;
using WebBattler.DAL.Models;

namespace WebBattler.Services.Interfaces;

public interface IProvinceService
{
    public void Create(ProvinceDTO province);
    public void Update(ProvinceDTO province);
    public void Delete(ProvinceDTO province);
    public List<ProvinceModel> GetAll();
}
