using WebBattler.DAL.Entities;

namespace WebBattler.DAL.Interfaces;

public interface IGameSessionRepository
{
    void Create(GameSessionEntity session);
    GameSessionEntity? GetByGuildId(ulong guildId);
    GameSessionEntity? GetById(int id);
    IReadOnlyCollection<GameSessionEntity> GetAll();
    IReadOnlyCollection<GameSessionEntity> GetAllByAdminUserId(ulong adminUserId);
    void Update(GameSessionEntity session);
}
