using WebBattler.DAL.Models;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;

namespace WebBattler.DAL.Repositories;

public class UnitRepository : IUnitRepository
{
    private readonly AutobattlerDbContext _context;

    public UnitRepository(AutobattlerDbContext context)
    {
        _context = context;
    }

    public void Create(UnitEntity unitEntity)
    {
        _context.Units.Add(unitEntity);
    }

    public void Delete(string name)
    {
        var soldier = _context.Units.FirstOrDefault(x => x.Name == name);

        if (soldier != null)
        {
            _context.Units.Remove(soldier);
        }
        else
        {
            Console.WriteLine("There is no soldier with this name");
        }
    }

    public List<UnitModel> ShowAll()
    {
        return _context.Units
            .Select(unit => new UnitModel
            {
                Name = unit.Name,
                Health = unit.Health,
                Weapon = unit.Weapon
            })
            .ToList();
    }
}
