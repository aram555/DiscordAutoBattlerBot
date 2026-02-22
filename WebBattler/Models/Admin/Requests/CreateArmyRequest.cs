using System.ComponentModel.DataAnnotations;

namespace WebBattler.Models.Admin.Requests;

public class CreateArmyRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Range(1, ulong.MaxValue)]
    public ulong OwnerId { get; set; }

    [Required]
    public string CountryName { get; set; } = string.Empty;

    [Required]
    public string ProvinceName { get; set; } = string.Empty;

    public string? CityName { get; set; }
    public string? ParentName { get; set; }
}
