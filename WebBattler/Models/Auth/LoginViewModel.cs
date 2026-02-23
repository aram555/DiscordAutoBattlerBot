using System.ComponentModel.DataAnnotations;

namespace WebBattler.Models.Auth;

public class LoginViewModel
{
    [Required(ErrorMessage = "Введите Discord User ID")]
    public ulong DiscordUserId { get; set; }

    [Required(ErrorMessage = "Введите пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}
