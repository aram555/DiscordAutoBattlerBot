using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBattler.DAL;
using WebBattler.DAL.DTO;
using WebBattler.Models.Admin;
using WebBattler.Models.Admin.Requests;
using WebBattler.Services.Interfaces;

namespace WebBattler.Controllers;

[Controller]
[Route("[controller]")]
public class AdminController : Controller
{
    private readonly IGameSessionService _gameSessionService;
    private readonly ICountryService _countryService;
    private readonly IProvinceService _provinceService;
    private readonly ICityService _cityService;
    private readonly IArmyService _armyService;
    private readonly IUnitService _unitService;
    private readonly IUnitSampleService _unitSampleService;
    private readonly IBuildingSampleService _buildingSampleService;

    public AdminController(
        IGameSessionService gameSessionService,
        ICountryService countryService,
        IProvinceService provinceService,
        ICityService cityService,
        IArmyService armyService,
        IUnitService unitService,
        IUnitSampleService unitSampleService,
        IBuildingSampleService buildingSampleService,
        AutobattlerDbContext dbContext)
    {
        _gameSessionService = gameSessionService;
        _countryService = countryService;
        _provinceService = provinceService;
        _cityService = cityService;
        _armyService = armyService;
        _unitService = unitService;
        _unitSampleService = unitSampleService;
        _buildingSampleService = buildingSampleService;
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
        var success = _gameSessionService.SetActive(id, isActive);
        TempData["StatusMessage"] = success
            ? (isActive ? "Сессия активирована." : "Сессия остановлена.")
            : "Сессия не найдена.";

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Session/{id:int}")]
    public IActionResult Session(int id)
    {
        var session = _gameSessionService.GetById(id);
        if (session == null)
        {
            TempData["StatusMessage"] = "Сессия не найдена.";
            return RedirectToAction(nameof(Index));
        }

        var countries = _countryService.GetAllBySessionId(id);

        var provinces = countries.SelectMany(c => c.Provinces).ToList();
        var cities = provinces.SelectMany(p => p.Cities).ToList();
        var countryNames = countries.Select(c => c.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
        var armies = _armyService.GetAll()
            .Where(a => a.Country != null && countryNames.Contains(a.Country.Name))
            .ToList();

        var model = new SessionManagementViewModel
        {
            SessionId = id,
            SessionName = session.Name,
            Countries = countries,
            Provinces = provinces,
            Cities = cities,
            Armies = armies
        };

        return View(model);
    }

    [HttpPost("Session/{sessionId:int}/Country")]
    [ValidateAntiForgeryToken]
    public IActionResult CreateCountry(int sessionId, CreateCountryRequest request)
    {
        _countryService.Create(new CountryDTO
        {
            Name = request.Name.Trim(),
            Description = request.Description?.Trim() ?? string.Empty,
            OwnerId = request.OwnerId,
            GameSessionId = sessionId,
            Money = request.Money,
            Provinces = new List<ProvinceDTO>()
        });

        TempData["StatusMessage"] = "Страна создана.";
        return RedirectToAction(nameof(Session), new { id = sessionId });
    }

    [HttpPost("Session/{sessionId:int}/Province")]
    [ValidateAntiForgeryToken]
    public IActionResult CreateProvince(int sessionId, CreateProvinceRequest request)
    {
        _provinceService.Create(new ProvinceDTO
        {
            Name = request.Name.Trim(),
            Description = request.Description?.Trim() ?? string.Empty,
            OwnerId = request.OwnerId,
            CountryName = request.CountryName,
            Cities = new List<CityDTO>(),
            Neighbours = new List<ProvinceDTO>()
        });

        TempData["StatusMessage"] = "Провинция создана.";
        return RedirectToAction(nameof(Session), new { id = sessionId });
    }

    [HttpPost("Session/{sessionId:int}/City")]
    [ValidateAntiForgeryToken]
    public IActionResult CreateCity(int sessionId, CreateCityRequest request)
    {
        _cityService.Create(new CityDTO
        {
            Name = request.Name.Trim(),
            Description = request.Description?.Trim() ?? string.Empty,
            OwnerId = request.OwnerId,
            Population = request.Population,
            Level = request.Level,
            ProvinceName = request.ProvinceName,
            IsCapital = request.IsCapital,
            Buildings = new List<BuildingDTO>()
        });

        TempData["StatusMessage"] = "Город создан.";
        return RedirectToAction(nameof(Session), new { id = sessionId });
    }

    [HttpPost("Session/{sessionId:int}/Neighbour")]
    [ValidateAntiForgeryToken]
    public IActionResult AddNeighbour(int sessionId, AddNeighbourRequest request)
    {
        TempData["StatusMessage"] = _provinceService.AddNeightbour(request.ProvinceName, request.NeighbourName);
        return RedirectToAction(nameof(Session), new { id = sessionId });
    }

    [HttpPost("Session/{sessionId:int}/Army")]
    [ValidateAntiForgeryToken]
    public IActionResult CreateArmy(int sessionId, CreateArmyRequest request)
    {
        _armyService.Create(new ArmyDTO
        {
            Name = request.Name.Trim(),
            OwnerId = request.OwnerId,
            CountryName = request.CountryName,
            ProvinceName = request.ProvinceName,
            CityName = string.IsNullOrWhiteSpace(request.CityName) ? null : request.CityName,
            ParentName = string.IsNullOrWhiteSpace(request.ParentName) ? null : request.ParentName,
            Units = new List<UnitDTO>()
        });

        TempData["StatusMessage"] = "Армия создана.";
        return RedirectToAction(nameof(Session), new { id = sessionId });
    }

    [HttpPost("Session/{sessionId:int}/Unit")]
    [ValidateAntiForgeryToken]
    public IActionResult CreateUnit(int sessionId, CreateUnitRequest request)
    {
        _unitService.Create(new UnitDTO
        {
            Name = request.Name.Trim(),
            OwnerId = request.OwnerId,
            Health = request.Health,
            Weapon = request.Weapon.Trim(),
            ArmyName = request.ArmyName
        });

        TempData["StatusMessage"] = "Юнит создан.";
        return RedirectToAction(nameof(Session), new { id = sessionId });
    }

    [HttpPost("Session/{sessionId:int}/UnitSample")]
    [ValidateAntiForgeryToken]
    public IActionResult CreateUnitSample(int sessionId, CreateUnitSampleRequest request)
    {
        _unitSampleService.Create(new UnitSampleDTO
        {
            Name = request.Name.Trim(),
            OwnerId = request.OwnerId,
            Health = request.Health,
            Weapon = request.Weapon.Trim(),
            BuildTurns = request.BuildTurns,
            Cost = request.Cost,
            Countryname = request.CountryName
        });

        TempData["StatusMessage"] = "Шаблон юнита создан.";
        return RedirectToAction(nameof(Session), new { id = sessionId });
    }

    [HttpPost("Session/{sessionId:int}/BuildingSample")]
    [ValidateAntiForgeryToken]
    public IActionResult CreateBuildingSample(int sessionId, CreateBuildingSampleRequest request)
    {
        _buildingSampleService.Create(new BuildingSampleDTO
        {
            Name = request.Name.Trim(),
            Description = request.Description?.Trim() ?? string.Empty,
            OwnerId = request.OwnerId,
            Level = request.Level,
            Cost = request.Cost,
            Income = request.Income,
            BuildTurns = request.BuildTurns,
            CountryName = request.CountryName
        });

        TempData["StatusMessage"] = "Шаблон здания создан.";
        return RedirectToAction(nameof(Session), new { id = sessionId });
    }
}
