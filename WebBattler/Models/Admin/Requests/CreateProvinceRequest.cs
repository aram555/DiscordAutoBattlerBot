using System.ComponentModel.DataAnnotations;

namespace WebBattler.Models.Admin.Requests;

public class CreateProvinceRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Range(1, ulong.MaxValue)]
    public ulong OwnerId { get; set; }

    [Required]
    public string CountryName { get; set; } = string.Empty;
}
