using System.Text;
using WebBattler.DAL.DTO;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using WebBattler.DAL.Models;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Services;

public class GameSessionService : IGameSessionService
{
    private readonly IGameSessionRepository _repository;
    private readonly IArmyService _armyService;
    private readonly IProductionOrderService _productionOrderService;
    private readonly ICountryService _countryService;

    public GameSessionService(
        IGameSessionRepository repository,
        IArmyService armyService,
        IProductionOrderService productionOrderService,
        ICountryService countryService)
    {
        _repository = repository;
        _armyService = armyService;
        _productionOrderService = productionOrderService;
        _countryService = countryService;
    }

    public GameSessionModel Create(GameSessionDTO dto)
    {
        var existing = _repository
            .GetAll()
            .FirstOrDefault(sesssion => sesssion.GuildId == dto.GuildId && sesssion.AdminUserId == dto.AdminUserId);

        if (existing != null)
        {
            existing.Name = dto.Name;
            existing.IsActive = true;

            _repository.Update(existing);
            return _Map(existing);
        }

        var entity = new GameSessionEntity
        {
            AdminUserId = dto.AdminUserId,
            GuildId = dto.GuildId,
            Name = dto.Name,
            CurrentTurn = 1,
            CurrentYear = 1,
            IsActive = true
        };

        _repository.Create(entity);

        return _Map(entity);
    }

    public GameSessionModel? GetByGuildId(ulong guildId)
    {
        var entity = _repository.GetByGuildId(guildId);
        return entity == null ? null : _Map(entity);
    }

    public GameSessionModel? GetById(int id)
    {
        var entity = _repository.GetById(id);
        return entity == null ? null : _Map(entity);
    }

    public IReadOnlyCollection<GameSessionModel> GetAll()
    {
        return _repository
            .GetAll()
            .Select(_Map)
            .ToList();
    }

    public IReadOnlyCollection<GameSessionModel> GetAllByAdminUserId(ulong adminUserId)
    {
        return _repository
            .GetAllByAdminUserId(adminUserId)
            .Select(_Map)
            .ToList();
    }

    public string EndTurn(int gameSessionId)
    {
        var session = GetById(gameSessionId);
        if (session == null)
        {
            return "Сессия не найдена.";
        }

        var battleLog = _armyService.ResolveAutomaticBattlesForAllProvinces();
        AdvanceTurn(gameSessionId);
        _armyService.ResetMovementPointsForAllArmies();

        var updated = GetById(gameSessionId);
        if (updated == null)
        {
            return "Сессия не найдена после обновления хода.";
        }

        var productionLog = _productionOrderService.ProcessTurn(gameSessionId, updated.CurrentTurn);
        var incomeLog = _countryService.ApplyIncomeForTurn(gameSessionId);

        var response = new StringBuilder();
        response.AppendLine($"Новый ход: {updated.CurrentTurn}");

        if(!string.IsNullOrWhiteSpace(battleLog))
        {
            response.AppendLine("Битвы:");
            response.AppendLine(battleLog);
        }
        if (!string.IsNullOrWhiteSpace(productionLog))
        {
            response.AppendLine("Производство:");
            response.AppendLine(productionLog);
        }

        if (!string.IsNullOrWhiteSpace(incomeLog))
        {
            response.AppendLine("Доходы:");
            response.AppendLine(incomeLog);
        }

        return response.ToString();
    }

    public void AdvanceTurn(int gameSessionId)
    {
        var entity = _repository.GetById(gameSessionId);
        if (entity == null || !entity.IsActive)
        {
            return;
        }

        entity.CurrentTurn++;
        entity.CurrentYear = entity.CurrentTurn;
        _repository.Update(entity);
    }

    public bool SetActive(int gameSessionId, bool isActive)
    {
        var entity = _repository.GetById(gameSessionId);
        if(entity == null)
        {
            return false;
        }

        entity.IsActive = isActive;
        _repository.Update(entity);
        return true;
    }

    private static GameSessionModel _Map(GameSessionEntity e) => new()
    {
        Id = e.Id,
        AdminUserId = e.AdminUserId,
        GuildId = e.GuildId,
        Name = e.Name,
        CurrentTurn = e.CurrentTurn,
        CurrentYear = e.CurrentYear,
        IsActive = e.IsActive
    };
}
