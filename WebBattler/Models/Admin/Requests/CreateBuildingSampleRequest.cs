using System.ComponentModel.DataAnnotations;

namespace WebBattler.Models.Admin.Requests;

public class CreateBuildingSampleRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Range(1, ulong.MaxValue)]
    public ulong OwnerId { get; set; }

    [Range(1, int.MaxValue)]
    public int Level { get; set; }

    [Range(1, int.MaxValue)]
    public int Cost { get; set; }

    [Range(0, int.MaxValue)]
    public int Income { get; set; }

    [Range(1, int.MaxValue)]
    public int BuildTurns { get; set; }

    [Required]
    public string CountryName { get; set; } = string.Empty;
}
