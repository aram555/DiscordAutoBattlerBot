using WebBattler.Services.Interfaces;
using WebBattler.DAL.Interfaces;
using WebBattler.DAL.Entities;
using WebBattler.DAL.DTO;
using System.Text;

namespace WebBattler.Services.Services;

public class ProductionOrderService : IProductionOrderService
{
    private readonly IProductionOrderRepository _repository;
    private readonly IBuildingSampleRepository _buildingSampleRepository;
    private readonly IUnitSampleRepository _unitSampleRepository;
    private readonly IGameSessionRepository _gameSessionRepository;
    private readonly IArmyRepository _armyRepository;
    private readonly ICityRepository _cityRepository;

    private readonly IUnitService _unitService;
    private readonly IBuildingService _buildingService;

    public ProductionOrderService(
        IProductionOrderRepository repository,
        IBuildingSampleRepository buildingSampleRepository,
        IUnitSampleRepository unitSampleRepository,
        IGameSessionRepository gameSessionRepository,
        IArmyRepository armyRepository,
        IUnitService unitService,
        IBuildingService buildingService,
        ICityRepository cityRepository)
    {
        _repository = repository;
        _buildingSampleRepository = buildingSampleRepository;
        _unitSampleRepository = unitSampleRepository;
        _gameSessionRepository = gameSessionRepository;
        _armyRepository = armyRepository;
        _unitService = unitService;
        _buildingService = buildingService;
        _cityRepository = cityRepository;
    }

    public void Queue(ProductionOrderDTO productionOrderDTO)
    {
        var product = new ProductionOrderEntity()
        {
            OwnerId = productionOrderDTO.OwnerId,
            GameSessionId = productionOrderDTO.GameSessionId,
            OrderType = productionOrderDTO.OrderType,
            Quantity = productionOrderDTO.Quantity,
            UnitSampleId = productionOrderDTO.UnitSampleId,
            BuildingSampleId = productionOrderDTO.BuildingSampleId,
            ArmyId = productionOrderDTO.ArmyId,
            CityId = productionOrderDTO.CityId,
            StartTurn = _gameSessionRepository.GetById(productionOrderDTO.GameSessionId)?.CurrentTurn ?? 0,
            ReadyTurn = _gameSessionRepository.GetById(productionOrderDTO.GameSessionId)?.CurrentTurn + productionOrderDTO.BuildTurns ?? 1
        };

        _repository.Create(product);
    }

    public string ProcessTurn(int gameSessionId, int currentTurn)
    {
        var readyOrders = _repository.GetReady(gameSessionId, currentTurn);
        var sb = new StringBuilder();

        foreach (var order in readyOrders)
        {
            if (order.OrderType == "Unit" && order.UnitSampleId.HasValue && order.ArmyId.HasValue)
            {
                var sample = _unitSampleRepository.GetById(order.UnitSampleId.Value);
                var army = _armyRepository.GetById(order.ArmyId.Value);

                if (sample != null && army != null)
                {
                    for (int i = 0; i < order.Quantity; i++)
                    {
                        _unitService.Create(new UnitDTO
                        {
                            Name = sample.Name,
                            Health = sample.Health,
                            Weapon = sample.Weapon,
                            OwnerId = sample.OwnerId,
                            ArmyName = army.Name
                        });
                    }

                    sb.AppendLine($"Произведено {order.Quantity} юнитов '{sample.Name}' для армии '{army.Name}'");
                }
            }

            if (order.OrderType == "Building" && order.BuildingSampleId.HasValue && order.CityId.HasValue)
            {
                var sample = _buildingSampleRepository.GetById(order.BuildingSampleId.Value);
                var city = _cityRepository.GetById(order.CityId.Value);

                if (sample != null && city != null)
                {
                    _buildingService.Create(new BuildingDTO
                    {
                        Name = sample.Name,
                        Description = sample.Description,
                        Level = sample.Level,
                        Cost = sample.Cost,
                        OwnerId = sample.OwnerId,
                        Profit = sample.Profit,
                        CityName = city?.Name
                    });

                    sb.AppendLine($"Построено {sample.Name} здание");
                }
                else
                {
                    order.Status = "Failed";
                    sb.AppendLine("Ошибка: Не удалось найти образец здания или город для заказа");
                    continue;
                }
            }

            order.Status = "Completed";
            _repository.Update(order);
        }

        return sb.ToString();
    }
}
