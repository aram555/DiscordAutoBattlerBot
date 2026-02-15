using Microsoft.EntityFrameworkCore;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;

namespace WebBattler.DAL.Repositories;

public class ProductionOrderRepository : IProductionOrderRepository
{
    private readonly AutobattlerDbContext _context;

    public ProductionOrderRepository(AutobattlerDbContext context)
    {
        _context = context;
    }

    public void Create(ProductionOrderEntity order)
    {
        _context.ProductionOrders.Add(order);
        _context.SaveChanges();
    }

    public List<ProductionOrderEntity> GetReady(int gameSessionId, int currentTurn)
    {
        return _context.ProductionOrders
            .Where(o => o.GameSessionId == gameSessionId && o.Status == "Queued" && o.ReadyTurn <= currentTurn)
            .Include(o => o.UnitSample)
            .Include(o => o.BuildingSample)
            .Include(o => o.Army)
            .Include(o => o.City)
            .ToList();
    }

    public void Update(ProductionOrderEntity order)
    {
        _context.SaveChanges();
    }
}
