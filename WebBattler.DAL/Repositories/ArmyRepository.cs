using WebBattler.DAL.Models;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;

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
    }

    public void Delete(ArmyEntity army)
    {
        _context.Armies.Remove(army);
    }

    public List<ArmyModel> GetAll()
    {
        return _context.Armies
            .Select(a => new ArmyModel
            {
                Name = a.Name,
                Units = a.Units.Select(u => new UnitModel
                {
                    Name = u.Name,
                    Health = u.Health,
                    Weapon = u.Weapon,
                }).ToList()
            }).ToList();
    }

    public int GetIdByName(string name)
    {
        var army = _context.Armies.FirstOrDefault(a => a.Name == name);
        return army != null ? army.Id : -1;
    }

    public void Update(ArmyEntity army)
    {
        throw new NotImplementedException();
    }
}
