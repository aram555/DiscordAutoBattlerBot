using WebBattler.DAL.DTO;
using WebBattler.DAL.Models;

namespace WebBattler.Services.Interfaces;

public interface IGameSessionService
{
    GameSessionModel Create(GameSessionDTO dto);
    GameSessionModel? GetByGuildId(ulong guildId);
    GameSessionModel? GetById(int id);
    IReadOnlyCollection<GameSessionModel> GetAll();
    public string EndTurn(int gameSessionId);
    void AdvanceTurn(int gameSessionId);
    bool SetActive(int gameSessionId, bool isActive);
}
