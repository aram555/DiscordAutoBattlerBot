using WebBattler.DAL.Entities;

namespace WebBattler.DAL.Interfaces;

public interface IProvinceRepository
{
    public void Create(ProvinceEntity province);
    public void Update(ProvinceEntity city);
    public void Delete(ProvinceEntity city);
    public int GetIdByName(string name);
    public ProvinceEntity GetById(int id);
    public List<ProvinceEntity> GetAll(ulong ownerId);
}
