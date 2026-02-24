using System.ComponentModel.DataAnnotations;

namespace WebBattler.Models.Admin.Requests;

public class CreateUnitSampleRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Range(1, ulong.MaxValue)]
    public ulong OwnerId { get; set; }

    [Range(0.1, int.MaxValue)]
    public float Health { get; set; }

    [Required]
    public string Weapon { get; set; } = string.Empty;

    [Range(0.1, float.MaxValue)]
    public float Damage { get; set; }

    [Range(0.1, float.MaxValue)]
    public float Armor { get; set; }

    [Range(1, int.MaxValue)]
    public int BuildTurns { get; set; }

    [Range(1, int.MaxValue)]
    public int Cost { get; set; }

    [Required]
    public string CountryName { get; set; } = string.Empty;
}
