using Microsoft.EntityFrameworkCore;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using WebBattler.DAL.Models;

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
        _context.SaveChanges();
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

    public void Update(UnitEntity unit)
    {
        _context.SaveChanges();
    }

    public int GetIdByName(string name)
    {
        return _context.Units.FirstOrDefault(u => u.Name == name).Id;
    }

    public UnitEntity GetById(int id)
    {
        return _context.Units.FirstOrDefault(b => b.Id == id);
    }

    public List<UnitEntity> GetAll(ulong ownerId)
    {
        return _context.Units
            .Where(a => a.OwnerId == ownerId)
            .ToList();
    }
}
