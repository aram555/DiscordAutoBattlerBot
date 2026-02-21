using Microsoft.AspNetCore.Mvc;
using WebBattler.DAL.DTO;
using WebBattler.Models.Admin;
using WebBattler.Services.Interfaces;

namespace WebBattler.Controllers;

[Controller]
[Route("[controller]")]
public class AdminController : Controller
{
    private readonly IGameSessionService _gameSessionService;

    public AdminController(IGameSessionService gameSessionService)
    {
        _gameSessionService = gameSessionService;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        var model = new AdminDashboardViewModel
        {
            Sessions = _gameSessionService.GetAll()
        };

        return View(model);
    }


    [HttpGet("Session")]
    public IActionResult CreateSession()
    {
        TempData["StatusMessage"] = "Используйте форму на странице админ-панели для создания сессии.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("Session")]
    [ValidateAntiForgeryToken]
    public IActionResult CreateSession([Bind(Prefix = "CreateForm")] CreateSessionRequest request)
    {
        if (!ModelState.IsValid)
        {
            var model = new AdminDashboardViewModel
            {
                Sessions = _gameSessionService.GetAll(),
                CreateForm = request
            };

            return View("Index", model);
        }

        _gameSessionService.Create(new GameSessionDTO
        {
            GuildId = request.GuildId,
            Name = request.Name.Trim()
        });

        TempData["StatusMessage"] = "Сессия сохранена.";
        return RedirectToAction(nameof(Index));
    }


    [HttpGet("AdvanceTurn")]
    [ActionName("AdvanceTurn")]
    public IActionResult AdvanceTurnGet(int id)
    {
        TempData["StatusMessage"] = "Переход хода выполняется только кнопкой на админ-панели.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("AdvanceTurn")]
    [ValidateAntiForgeryToken]
    public IActionResult AdvanceTurn(int id)
    {
        _gameSessionService.AdvanceTurn(id);
        TempData["StatusMessage"] = "Ход обновлён.";
        return RedirectToAction(nameof(Index));
    }


    [HttpGet("ToggleSession")]
    [ActionName("ToggleSession")]
    public IActionResult ToggleSessionGet(int id, bool isActive)
    {
        TempData["StatusMessage"] = "Изменение статуса доступно только через кнопки на админ-панели.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("ToggleSession")]
    [ValidateAntiForgeryToken]
    public IActionResult ToggleSession(int id, bool isActive)
    {
        var session = _gameSessionService.GetById(id);
        if (session == null)
        {
            TempData["StatusMessage"] = "Сессия не найдена.";
            return RedirectToAction(nameof(Index));
        }

        var nextState = !session.IsActive;
        _gameSessionService.SetActive(id, nextState);
        TempData["StatusMessage"] = nextState ? "Сессия активирована." : "Сессия остановлена.";

        return RedirectToAction(nameof(Index));
    }
}
