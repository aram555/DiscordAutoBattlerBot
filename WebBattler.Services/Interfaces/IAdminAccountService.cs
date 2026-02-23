using WebBattler.DAL.Models;

namespace WebBattler.Services.Interfaces;

public interface IAdminAccountService
{
    AdminAccountModel? GetByDiscordUserId(ulong discordUserId);
    bool ExistsByDiscordUserId(ulong discordUserId);
    AdminAccountModel Create(ulong discordUserId, string displayName, string password);
    bool VerifyPassword(AdminAccountModel account, string password);
}
