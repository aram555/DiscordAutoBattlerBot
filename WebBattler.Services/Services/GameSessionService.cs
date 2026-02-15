using WebBattler.DAL.DTO;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using WebBattler.DAL.Models;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Services;

public class GameSessionService : IGameSessionService
{
    private readonly IGameSessionRepository _repository;

    public GameSessionService(IGameSessionRepository repository)
    {
        _repository = repository;
    }

    public GameSessionModel Create(GameSessionDTO dto)
    {
        var existing = _repository.GetByGuildId(dto.GuildId);
        if (existing != null)
        {
            return _Map(existing);
        }

        var entity = new GameSessionEntity
        {
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

    public void AdvanceTurn(int gameSessionId)
    {
        var entity = _repository.GetById(gameSessionId);
        if (entity == null)
        {
            return;
        }

        entity.CurrentTurn++;
        entity.CurrentYear = entity.CurrentTurn;
        _repository.Update(entity);
    }

    private static GameSessionModel _Map(GameSessionEntity e) => new()
    {
        Id = e.Id,
        GuildId = e.GuildId,
        Name = e.Name,
        CurrentTurn = e.CurrentTurn,
        CurrentYear = e.CurrentYear,
        IsActive = e.IsActive
    };
}
