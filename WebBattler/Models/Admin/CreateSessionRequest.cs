using System.ComponentModel.DataAnnotations;

namespace WebBattler.Models.Admin;

public class CreateSessionRequest
{
    [Required(ErrorMessage = "Введите ID Discord сервера")]
    [Range(1, ulong.MaxValue, ErrorMessage = "ID сервера должен быть положительным")]
    public ulong GuildId { get; set; }

    [Required(ErrorMessage = "Введите название сессии")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Название должно быть от 3 до 100 символов")]
    public string Name { get; set; } = string.Empty;
}
