using WebBattler.DAL.Entities;

namespace WebBattler.DAL.Interfaces;

public interface IGameSessionRepository
{
    void Create(GameSessionEntity session);
    GameSessionEntity? GetByGuildId(ulong guildId);
    GameSessionEntity? GetById(int id);
    void Update(GameSessionEntity session);
}
