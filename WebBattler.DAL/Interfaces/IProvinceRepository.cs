using WebBattler.DAL.Entities;

namespace WebBattler.DAL.Interfaces;

public interface IProvinceRepository
{
    public void Create(ProvinceEntity country);
    public void Update(ProvinceEntity country);
    public void Delete(ProvinceEntity country);
    public List<ProvinceEntity> GetAll();
}
