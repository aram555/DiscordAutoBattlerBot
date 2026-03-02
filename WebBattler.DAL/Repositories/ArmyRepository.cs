using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text;

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

    public void Delete(string armyName)
    {
        if(string.IsNullOrEmpty(armyName))
        {
            return;
        }

        var army = _context.Armies.FirstOrDefault(ar => ar.Name == armyName);

        if(army != null)
        {
            return;
        }

        _context.Armies.Remove(army);
        _context.SaveChanges();
    }

    public void Update(ArmyEntity army)
    {
        _context.Armies.Update(army);
        _context.SaveChanges();
    }

    public bool TryMoveToProvince(string armyName, string provinceName)
    {
        var army = _context.Armies.FirstOrDefault(a => a.Name == armyName);

        if(army == null )
        {
            return false;
        }

        if(army.CurrentTurnCount <= 0)
        {
            return false;
        }

        var province = _context.Provinces.FirstOrDefault(p => p.Name == provinceName);
        if (province == null)
        {
            return false;
        }

        army.ProvinceId = province.Id;
        army.CurrentTurnCount -= 1;
        _context.SaveChanges();

        return true;
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

    public List<ArmyEntity> GetAll()
    {
        return _context.Armies
            .Include(p => p.Province)
                .ThenInclude(n => n.Neighbours)
            .Include(a => a.Units)
            .Include(a => a.Country)
            .ToList();
    }

    public void ResetMovementPointsForAllArmies()
    {
        var armies = _context.Armies.ToList();

        foreach (var army in armies)
        {
            army.CurrentTurnCount = ArmyEntity.MaxTurnCount;
        }

        _context.SaveChanges();
    }

    public string HealSoldiersInAllarmiers(int sessionId)
    {
        var armies = _context.Armies
            .Include(a => a.Country)
            .Where(a => a.Country.GameSessionId == sessionId && a.Status == "Waiting")
            .Include(a => a.Units)
            .ToList();

        var sb = new StringBuilder();

        foreach (var army in armies)
        {
            foreach (var unit in army.Units)
            {
                unit.Health = unit.MaxHealth;
                sb.Append($"{unit.Name} восполнил своё здоровье до максимума");
            }
        
        }

        _context.SaveChanges();
        return sb.ToString();
    }
}
