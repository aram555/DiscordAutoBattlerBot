using WebBattler.DAL.DTO;
using WebBattler.Models.Admin;
using Microsoft.AspNetCore.Mvc;

namespace WebBattler.Controllers;

public partial class AdminController : Controller
{
    [HttpGet("Session/{id:int}/Edit")]
    public IActionResult EditSession(int id)
    {
        var session = _gameSessionService.GetById(id);
        if (session == null)
        {
            TempData["StatusMessage"] = "Сессия не найдена.";
            return RedirectToAction(nameof(Index));
        }

        var countries = _countryService.GetAllBySessionId(id).ToList();
        var provinces = countries.SelectMany(c => c.Provinces).ToList();
        var cities = provinces.SelectMany(p => p.Cities).ToList();
        var countryNames = countries.Select(c => c.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
        var armies = _armyService.GetAll()
            .Where(a => a.Country != null && countryNames.Contains(a.Country.Name))
            .ToList();
        var units = armies.SelectMany(a => a.Units ?? new List<WebBattler.DAL.Models.UnitModel>()).ToList();
        var unitSamples = _unitSampleService.GetAllBySessionId(id).ToList();
        var buildingSamples = _buildingSampleService.GetAllBySessionId(id).ToList();
        var buildings = cities.SelectMany(c => c.Buildings ?? new List<WebBattler.DAL.Models.BuildingModel>()).ToList();

        var model = new AdminSessionEditorViewModel
        {
            SessionId = id,
            SessionName = session.Name,
            Countries = countries,
            Provinces = provinces,
            Cities = cities,
            Armies = armies,
            Units = units,
            UnitSamples = unitSamples,
            Buildings = buildings,
            BuildingSamples = buildingSamples
        };

        return View(model);
    }

    [HttpPost("Session/{sessionId:int}/Edit/Country")]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateCountry(int sessionId, string originalName, string name, string? description, ulong ownerId, int money, int gameSessionId)
    {
        _countryService.Update(new CountryDTO
        {
            OriginalName = originalName,
            Name = name.Trim(),
            Description = description?.Trim() ?? string.Empty,
            OwnerId = ownerId,
            Money = money,
            GameSessionId = gameSessionId,
            Provinces = new List<ProvinceDTO>()
        });

        TempData["StatusMessage"] = "Страна обновлена.";
        return RedirectToAction(nameof(EditSession), new { id = sessionId });
    }

    [HttpPost("Session/{sessionId:int}/Edit/Province")]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateProvince(int sessionId, string originalName, string name, string? description, ulong ownerId, string countryName, List<string>? neighbourNames)
    {
        var neighbours = (neighbourNames ?? new List<string>())
            .Where(n => !string.IsNullOrWhiteSpace(n))
            .Select(n => new ProvinceDTO { Name = n })
            .ToList();

        _provinceService.Update(new ProvinceDTO
        {
            OriginalName = originalName,
            Name = name.Trim(),
            Description = description?.Trim() ?? string.Empty,
            OwnerId = ownerId,
            CountryName = countryName,
            Cities = new List<CityDTO>(),
            Neighbours = neighbours
        });

        TempData["StatusMessage"] = "Провинция обновлена.";
        return RedirectToAction(nameof(EditSession), new { id = sessionId });
    }

    [HttpPost("Session/{sessionId:int}/Edit/City")]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateCity(int sessionId, string originalName, string name, string? description, ulong ownerId, int population, int level, bool isCapital, string provinceName)
    {
        _cityService.Update(new CityDTO
        {
            OriginalName = originalName,
            Name = name.Trim(),
            Description = description?.Trim() ?? string.Empty,
            OwnerId = ownerId,
            Population = population,
            Level = level,
            IsCapital = isCapital,
            ProvinceName = provinceName,
            Buildings = new List<BuildingDTO>()
        });

        TempData["StatusMessage"] = "Город обновлён.";
        return RedirectToAction(nameof(EditSession), new { id = sessionId });
    }

    [HttpPost("Session/{sessionId:int}/Edit/Army")]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateArmy(int sessionId, string originalName, string name, string status, ulong ownerId, string countryName, string provinceName, string? cityName, string? parentName, int currentTurnCount)
    {
        _armyService.Update(new ArmyDTO
        {
            OriginalName = originalName,
            Name = name.Trim(),
            Status = status.Trim(),
            OwnerId = ownerId,
            CountryName = countryName,
            ProvinceName = provinceName,
            CityName = string.IsNullOrWhiteSpace(cityName) ? null : cityName,
            ParentName = string.IsNullOrWhiteSpace(parentName) ? null : parentName,
            CurrentTurnCount = currentTurnCount,
            Units = new List<UnitDTO>()
        });

        TempData["StatusMessage"] = "Армия обновлена.";
        return RedirectToAction(nameof(EditSession), new { id = sessionId });
    }

    [HttpPost("Session/{sessionId:int}/Edit/Unit")]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateUnit(int sessionId, string originalName, string name, ulong ownerId, float health, string weapon, string armyName)
    {
        _unitService.Update(new UnitDTO
        {
            OriginalName = originalName,
            Name = name.Trim(),
            OwnerId = ownerId,
            Health = health,
            Weapon = weapon.Trim(),
            ArmyName = armyName
        });

        TempData["StatusMessage"] = "Юнит обновлён.";
        return RedirectToAction(nameof(EditSession), new { id = sessionId });
    }

    [HttpPost("Session/{sessionId:int}/Edit/UnitSample")]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateUnitSample(int sessionId, string originalName, string name, ulong ownerId, float health, string weapon, int buildTurns, int cost, string countryName)
    {
        _unitSampleService.Update(new UnitSampleDTO
        {
            OriginalName = originalName,
            Name = name.Trim(),
            OwnerId = ownerId,
            Health = health,
            Weapon = weapon.Trim(),
            BuildTurns = buildTurns,
            Cost = cost,
            Countryname = countryName
        });

        TempData["StatusMessage"] = "Шаблон юнита обновлён.";
        return RedirectToAction(nameof(EditSession), new { id = sessionId });
    }

    [HttpPost("Session/{sessionId:int}/Edit/Building")]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateBuilding(int sessionId, string originalName, string name, string? description, ulong ownerId, int level, int cost, int profit, string cityName)
    {
        _buildingService.Update(new BuildingDTO
        {
            OriginalName = originalName,
            Name = name.Trim(),
            Description = description?.Trim() ?? string.Empty,
            OwnerId = ownerId,
            Level = level,
            Cost = cost,
            Profit = profit,
            CityName = cityName
        });

        TempData["StatusMessage"] = "Здание обновлено.";
        return RedirectToAction(nameof(EditSession), new { id = sessionId });
    }

    [HttpPost("Session/{sessionId:int}/Edit/BuildingSample")]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateBuildingSample(int sessionId, string originalName, string name, string? description, ulong ownerId, int level, int cost, int profit, int buildTurns, string countryName)
    {
        _buildingSampleService.Update(new BuildingSampleDTO
        {
            OriginalName = originalName,
            Name = name.Trim(),
            Description = description?.Trim() ?? string.Empty,
            OwnerId = ownerId,
            Level = level,
            Cost = cost,
            Income = profit,
            BuildTurns = buildTurns,
            CountryName = countryName
        });

        TempData["StatusMessage"] = "Шаблон здания обновлён.";
        return RedirectToAction(nameof(EditSession), new { id = sessionId });
    }
}
