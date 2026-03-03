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
        var armyNames = armies.Select(a => a.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
        var units = _unitService.GetAllBySessionId(id)
            .Where(u => u.Army != null && armyNames.Contains(u.Army.Name))
            .ToList();
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
        if (!IsSessionOwnedByCurrentUser(sessionId))
        {
            return Forbid();
        }

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
        if (!IsSessionOwnedByCurrentUser(sessionId))
        {
            return Forbid();
        }

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
        if (!IsSessionOwnedByCurrentUser(sessionId))
        {
            return Forbid();
        }

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
        if (!IsSessionOwnedByCurrentUser(sessionId))
        {
            return Forbid();
        }

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
    public IActionResult UpdateUnit(int sessionId, string originalName, string name, ulong ownerId, float health, float maxHealth, float damage, float armor, string weapon, string armyName)
    {
        if (!IsSessionOwnedByCurrentUser(sessionId))
        {
            return Forbid();
        }

        _unitService.Update(new UnitDTO
        {
            OriginalName = originalName,
            Name = name.Trim(),
            OwnerId = ownerId,
            Health = health,
            MaxHealth = maxHealth,
            Damage = damage,
            Armor = armor,
            Weapon = weapon.Trim(),
            ArmyName = armyName
        });

        TempData["StatusMessage"] = "Юнит обновлён.";
        return RedirectToAction(nameof(EditSession), new { id = sessionId });
    }

    [HttpPost("Session/{sessionId:int}/Edit/UnitSample")]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateUnitSample(int sessionId, string originalName, string name, ulong ownerId, float health, float damage, float armor, string weapon, int buildTurns, int cost, string countryName)
    {
        if (!IsSessionOwnedByCurrentUser(sessionId))
        {
            return Forbid();
        }

        _unitSampleService.Update(new UnitSampleDTO
        {
            OriginalName = originalName,
            Name = name.Trim(),
            OwnerId = ownerId,
            Health = health,
            Weapon = weapon.Trim(),
            Damage = damage,
            Armor = armor,
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
        if (!IsSessionOwnedByCurrentUser(sessionId))
        {
            return Forbid();
        }

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
        if (!IsSessionOwnedByCurrentUser(sessionId))
        {
            return Forbid();
        }

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
