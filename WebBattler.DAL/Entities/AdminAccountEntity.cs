namespace WebBattler.DAL.Entities;

public class AdminAccountEntity
{
    public int Id { get; set; }
    public ulong DiscordUserId { get; set; }
    public string DisplayName { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string PasswordSalt { get; set; } = null!;
    public DateTime CreatedAtUtc { get; set; }
}
