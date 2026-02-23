using WebBattler.Services.Interfaces;
using System.Security.Cryptography;
using WebBattler.DAL.Interfaces;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Models;

namespace WebBattler.Services.Services;

public class AdminAccountService : IAdminAccountService
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100_000;

    private readonly IAdminAccountRepository _repository;

    public AdminAccountService(IAdminAccountRepository repository)
    {
        _repository = repository;
    }

    public AdminAccountModel? GetByDiscordUserId(ulong discordUserId)
    {
        var entity = _repository.GetByDiscordUserId(discordUserId);
        return entity == null ? null : Map(entity);
    }

    public bool ExistsByDiscordUserId(ulong discordUserId)
    {
        return _repository.ExistsByDiscordUserId(discordUserId);
    }

    public AdminAccountModel Create(ulong discordUserId, string displayName, string password)
    {
        var (salt, hash) = HashPassword(password);

        var entity = new AdminAccountEntity
        {
            DiscordUserId = discordUserId,
            DisplayName = displayName.Trim(),
            PasswordSalt = salt,
            PasswordHash = hash,
            CreatedAtUtc = DateTime.UtcNow
        };

        _repository.Create(entity);
        return Map(entity);
    }

    public bool VerifyPassword(AdminAccountModel account, string password)
    {
        var saltBytes = Convert.FromBase64String(account.PasswordSalt);
        var hashBytes = Rfc2898DeriveBytes.Pbkdf2(password, saltBytes, Iterations, HashAlgorithmName.SHA256, HashSize);
        var expectedHashBytes = Convert.FromBase64String(account.PasswordHash);
        return CryptographicOperations.FixedTimeEquals(hashBytes, expectedHashBytes);
    }

    private static (string Salt, string Hash) HashPassword(string password)
    {
        var saltBytes = RandomNumberGenerator.GetBytes(SaltSize);
        var hashBytes = Rfc2898DeriveBytes.Pbkdf2(password, saltBytes, Iterations, HashAlgorithmName.SHA256, HashSize);
        return (Convert.ToBase64String(saltBytes), Convert.ToBase64String(hashBytes));
    }

    private static AdminAccountModel Map(AdminAccountEntity entity) => new()
    {
        Id = entity.Id,
        DiscordUserId = entity.DiscordUserId,
        DisplayName = entity.DisplayName,
        PasswordHash = entity.PasswordHash,
        PasswordSalt = entity.PasswordSalt
    };
}
