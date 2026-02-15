using WebBattler.DAL.DTO;
using WebBattler.DAL.Models;

namespace WebBattler.Services.Interfaces;

public interface IGameSessionService
{
    GameSessionModel Create(GameSessionDTO dto);
    GameSessionModel? GetByGuildId(ulong guildId);
    GameSessionModel? GetById(int id);
    void AdvanceTurn(int gameSessionId);
}
