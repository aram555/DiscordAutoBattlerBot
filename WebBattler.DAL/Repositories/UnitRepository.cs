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
        if (string.IsNullOrEmpty(name))
        {
            return;
        }

        var entity = _context.Units.FirstOrDefault(u => u.Name == name);

        if (entity != null)
        {
            return;
        }

        _context.Units.Remove(entity);
        _context.SaveChanges();
    }

    public void Update(UnitEntity unit)
    {
        var entity = _context.Units.FirstOrDefault(u => u.Name == unit.Name);

        if (entity == null)
        {
            return;
        }

        entity.Health = unit.Health;
        entity.Weapon = unit.Weapon;

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
