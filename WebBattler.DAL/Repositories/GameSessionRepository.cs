using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;

namespace WebBattler.DAL.Repositories;

public class GameSessionRepository : IGameSessionRepository
{
    private readonly AutobattlerDbContext _context;
    
    public GameSessionRepository(AutobattlerDbContext context)
    {
        _context = context;
    }

    public void Create(GameSessionEntity session)
    {
        _context.GameSessions.Add(session);
        _context.SaveChanges();
    }

    public GameSessionEntity? GetByGuildId(ulong guildId)
    {
        return _context.GameSessions.FirstOrDefault(s => s.GuildId == guildId && s.IsActive);
    }

    public GameSessionEntity? GetById(int id)
    {
        return _context.GameSessions.FirstOrDefault(s => s.Id == id);
    }

    public IReadOnlyCollection<GameSessionEntity> GetAll()
    {
        return _context.GameSessions
            .OrderByDescending(s => s.IsActive)
            .ThenBy(s => s.Name)
            .ToList();
    }

    public IReadOnlyCollection<GameSessionEntity> GetAllByAdminUserId(ulong adminUserId)
    {
        return _context.GameSessions
            .Where(s => s.AdminUserId == adminUserId)
            .OrderByDescending(s => s.IsActive)
            .ThenBy(s => s.Name)
            .ToList();
    }

    public void Update(GameSessionEntity session)
    {
        _context.SaveChanges();
    }
}
