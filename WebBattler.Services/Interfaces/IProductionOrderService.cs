using WebBattler.DAL.DTO;

namespace WebBattler.Services.Interfaces;

public interface IProductionOrderService
{
    void Queue(ProductionOrderDTO productionOrderDTO);
    string ProcessTurn(int gameSessionId, int currentTurn);
}
