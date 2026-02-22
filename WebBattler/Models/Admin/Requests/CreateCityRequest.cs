using System.ComponentModel.DataAnnotations;

namespace WebBattler.Models.Admin.Requests;

public class CreateCityRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Range(1, ulong.MaxValue)]
    public ulong OwnerId { get; set; }

    [Range(1, int.MaxValue)]
    public int Population { get; set; }

    [Range(1, int.MaxValue)]
    public int Level { get; set; } = 1;

    [Required]
    public string ProvinceName { get; set; } = string.Empty;

    public bool IsCapital { get; set; }
}
