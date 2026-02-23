using System.ComponentModel.DataAnnotations;

namespace WebBattler.Models.Auth;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Введите Discord User ID")]
    public ulong DiscordUserId { get; set; }

    [Required(ErrorMessage = "Введите отображаемое имя")]
    [MaxLength(64)]
    public string DisplayName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите пароль")]
    [MinLength(6, ErrorMessage = "Пароль должен быть не менее 6 символов")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Повторите пароль")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
