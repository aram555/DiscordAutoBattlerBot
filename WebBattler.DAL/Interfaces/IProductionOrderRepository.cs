using WebBattler.DAL.Entities;

namespace WebBattler.DAL.Interfaces;

public interface IProductionOrderRepository
{
    void Create(ProductionOrderEntity order);
    List<ProductionOrderEntity> GetReady(int gameSessionId, int currentTurn);
    void Update(ProductionOrderEntity order);
}
