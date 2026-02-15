namespace WebBattler.DAL.Models;

public class GameSessionModel
{
    public int Id { get; set; }
    public ulong GuildId { get; set; }
    public string Name { get; set; } = null!;
    public int CurrentTurn { get; set; }
    public int CurrentYear { get; set; }
    public bool IsActive { get; set; }
}
