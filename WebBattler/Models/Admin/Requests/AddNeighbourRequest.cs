using System.ComponentModel.DataAnnotations;

namespace WebBattler.Models.Admin.Requests;

public class AddNeighbourRequest
{
    [Required]
    public string ProvinceName { get; set; } = string.Empty;

    [Required]
    public string NeighbourName { get; set; } = string.Empty;
}
