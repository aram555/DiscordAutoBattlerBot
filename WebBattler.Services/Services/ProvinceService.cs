using Discord;
using WebBattler.DAL.DTO;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using WebBattler.DAL.Models;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Services;

public class ProvinceService : IProvinceService
{
    private readonly IProvinceRepository _repository;
    private readonly ICountryRepository _countryRepository;

    public ProvinceService(IProvinceRepository provinceRepository, ICountryRepository countryRepository)
    {
        _repository = provinceRepository;
        _countryRepository = countryRepository;
    }

    public void Create(ProvinceDTO province)
    {
        ProvinceEntity provinceEntity = new ProvinceEntity
        {
            Name = province.Name,
            CountryId = _countryRepository.GetIdByName(province.CountryName),
            Description = province.Description,
            Cities = province.Cities.Select(cityDto => new CityEntity
            {
                Name = cityDto.Name,
                Population = cityDto.Population,
                Buildings = cityDto.Buildings.Select(buildingDto => new BuildingEntity
                {
                    Name = buildingDto.Name,
                    Level = buildingDto.Level
                }).ToList()
            }).ToList(),

            Neighbours = new List<ProvinceEntity>()
        };

        _repository.Create(provinceEntity);
    }

    public void Delete(string provinceName)
    {
        _repository.Delete(provinceName);
    }

    public void Update(ProvinceDTO province)
    {
        var entity = _repository.GetById(_repository.GetIdByName(province.Name));
        if (entity == null)
        {
            return;
        }

        if(!string.IsNullOrWhiteSpace(province.Name))
        {
            entity.Name = province.Name;
        }

        if (!string.IsNullOrWhiteSpace(province.Description))
        {
            entity.Description = province.Description;
        }

        if (province.OwnerId != default)
        {
            entity.OwnerId = province.OwnerId;
        }

        if (!string.IsNullOrWhiteSpace(province.CountryName))
        {
            entity.CountryId = _countryRepository.GetIdByName(province.CountryName);
        }

        _repository.Update(entity);
    }

    public string AddNeightbour(string provinceName, string neighbourName)
    {
        var province = _repository.GetById(_repository.GetIdByName(provinceName));
        var neighbour = _repository.GetById(_repository.GetIdByName(neighbourName));

        if (province.Id == neighbour.Id)
        {
            return "Провинция не может быть соседом сама для себя.";
        }

        if(province == null || neighbour == null)
        {
            return "Одна из указанных провинций не найдена.";
        }

        _repository.AddNeightbour(provinceName, neighbourName);
        return "Сосед успешно добавлен.";
    }

    public int GetIdByName(string name)
    {
        return _repository.GetIdByName(name);
    }

    public ProvinceModel GetById(int id)
    {
        var entity = _repository.GetById(id);

        Console.WriteLine(entity.Neighbours.Count);

        return new ProvinceModel()
        {
            Name = entity.Name,
            Description = entity.Description,
            OwnerId = entity.OwnerId,
            Neighbours = entity.Neighbours.Select(n => new ProvinceModel()
            {
                Name = n.Name,
                Description = n.Description,
                OwnerId = n.OwnerId,
            }).ToList(),
            Cities = entity.Cities.Select(c => new CityModel()
            {
                Name = c.Name,
                Description = c.Description,
                Level = c.Level,
                Population = c.Population,
                OwnerId = c.OwnerId,
                Buildings = c.Buildings.Select(c => new BuildingModel()
                {
                    Name = c.Name,
                    Description = c.Description,
                    Cost = c.Cost,
                    Level = c.Level,
                    OwnerId = c.OwnerId,
                }).ToList()
            }).ToList()
        };
    }

    public List<ProvinceModel> GetAll(ulong ownerId)
    {
        var list = new List<ProvinceModel>();

        foreach(var entity in _repository.GetAll(ownerId))
        {
            list.Add(new ProvinceModel()
            {
                Name = entity.Name,
                Description = entity.Description,
                OwnerId = entity.OwnerId,
                Neighbours = entity.Neighbours.Select(n => new ProvinceModel()
                {
                    Name = n.Name,
                    Description = n.Description,
                    OwnerId = n.OwnerId,
                }).ToList(),
                Cities = entity.Cities.Select(c => new CityModel()
                {
                    Name = c.Name,
                    Description = c.Description,
                    Level = c.Level,
                    Population = c.Population,
                    OwnerId = c.OwnerId,
                    Buildings = c.Buildings.Select(c => new BuildingModel()
                    {
                        Name = c.Name,
                        Description = c.Description,
                        Cost = c.Cost,
                        Level = c.Level,
                        OwnerId = c.OwnerId,
                    }).ToList()
                }).ToList()
            });
        }

        return list;
    }

    public List<ProvinceModel> GetNeighbours(ulong ownerId)
    {
        return _repository.GetNeighbours(ownerId).Select(entity => new ProvinceModel()
        {
            Name = entity.Name,
            Description = entity.Description,
            OwnerId = entity.OwnerId,
            Neighbours = entity.Neighbours.Select(n => new ProvinceModel()
            {
                Name = n.Name,
                Description = n.Description,
                OwnerId = n.OwnerId,
            }).ToList(),
        }).ToList();
    }
}
