using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WebBattler.DAL.Repositories;

public class ArmyRepository : IArmyRepository
{
    private readonly AutobattlerDbContext _context;

    public ArmyRepository(AutobattlerDbContext context)
    {
        _context = context;
    }

    public void Create(ArmyEntity army)
    {
        _context.Armies.Add(army);
        _context.SaveChanges();
    }

    public void Delete(ArmyEntity army)
    {
        _context.Armies.Remove(army);
    }

    public void Update(ArmyEntity army)
    {
        throw new NotImplementedException();
    }

    public void MoveToProvince(string armyName, string provinceName)
    {
        var army = _context.Armies.FirstOrDefault(a => a.Name == armyName);
        if (army == null) return;

        var province = _context.Provinces.FirstOrDefault(p => p.Name == provinceName);
        if (province == null) return;

        army.ProvinceId = province.Id;
        _context.SaveChanges();
    }

    public int? GetIdByName(string name)
    {
        return _context.Armies.FirstOrDefault(a => a.Name == name)?.Id;
    }

    public ArmyEntity GetById(int id)
    {
        return _context.Armies.FirstOrDefault(x => x.Id == id);
    }

    public List<ArmyEntity> GetAll(ulong ownerId)
    {
        return _context.Armies
            .Where(a => a.OwnerId == ownerId)
            .Include(p => p.Province)
                .ThenInclude(n => n.Neighbours)
            .Include(a => a.Units)
            .Include(a => a.Country)
            .ToList();
    }

    public List<ArmyEntity> GetAllInProvince(int provinceId)
    {
        return _context.Armies
            .Where(a => a.ProvinceId == provinceId)
            .Include(p => p.Province)
                .ThenInclude(n => n.Neighbours)
            .Include(a => a.Units)
            .Include(a => a.Country)
            .ToList();
    }
}
