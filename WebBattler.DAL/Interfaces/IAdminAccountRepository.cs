using WebBattler.DAL.Entities;

namespace WebBattler.DAL.Interfaces;

public interface IAdminAccountRepository
{
    AdminAccountEntity? GetByDiscordUserId(ulong discordUserId);
    bool ExistsByDiscordUserId(ulong discordUserId);
    void Create(AdminAccountEntity entity);
}
