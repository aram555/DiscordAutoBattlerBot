using WebBattler.DAL.Models;
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
    }

    public void Delete(ArmyEntity army)
    {
        _context.Armies.Remove(army);
    }

    public void Update(ArmyEntity army)
    {
        throw new NotImplementedException();
    }

    public int GetIdByName(string name)
    {
        var army = _context.Armies.FirstOrDefault(a => a.Name == name);
        return army != null ? army.Id : -1;
    }

    public ArmyEntity GetById(int id)
    {
        return _context.Armies.FirstOrDefault(x => x.Id == id);
    }

    public List<ArmyEntity> GetAll(ulong ownerId)
    {
        return _context.Armies
            .Where(a => a.OwnerId == ownerId)
            .Include(a => a.Units)
            .ToList();
    }
}
