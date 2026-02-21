using WebBattler.DAL.Models;

namespace WebBattler.Models.Admin;

public class AdminDashboardViewModel
{
    public IReadOnlyCollection<GameSessionModel> Sessions { get; set; } = Array.Empty<GameSessionModel>();
    public CreateSessionRequest CreateForm { get; set; } = new();
}
