namespace WebBattler.DAL.Models;

public class AdminAccountModel
{
    public int Id { get; set; }
    public ulong DiscordUserId { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string PasswordSalt { get; set; } = string.Empty;
}
