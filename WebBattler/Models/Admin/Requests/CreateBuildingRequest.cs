using System.ComponentModel.DataAnnotations;

namespace WebBattler.Models.Admin.Requests;

public class CreateBuildingRequest
{
    [Required]
    public string SampleName { get; set; } = string.Empty;

    [Range(1, ulong.MaxValue)]
    public ulong OwnerId { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Required]
    public string CityName { get; set; } = string.Empty;
}
