using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;

namespace WebBattler.DAL.Repositories;

public class AdminAccountRepository : IAdminAccountRepository
{
    private readonly AutobattlerDbContext _context;

    public AdminAccountRepository(AutobattlerDbContext context)
    {
        _context = context;
    }

    public AdminAccountEntity? GetByDiscordUserId(ulong discordUserId)
    {
        return _context.AdminAccounts.FirstOrDefault(a => a.DiscordUserId == discordUserId);
    }

    public bool ExistsByDiscordUserId(ulong discordUserId)
    {
        return _context.AdminAccounts.Any(a => a.DiscordUserId == discordUserId);
    }

    public void Create(AdminAccountEntity entity)
    {
        _context.AdminAccounts.Add(entity);
        _context.SaveChanges();
    }
}
