using WebBattler.DAL.DTO;
using WebBattler.DAL.Models;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Services;

public class ProvinceService : IProvinceService
{
    private readonly IProvinceRepository _repository;

    public ProvinceService(IProvinceRepository provinceRepository)
    {
        _repository = provinceRepository;
    }

    public void Create(ProvinceDTO province)
    {
        ProvinceEntity provinceEntity = new ProvinceEntity
        {
            Name = province.Name,
            Cities = province.Cities.Select(cityDto => new CityEntity
            {
                Name = cityDto.Name,
                Population = cityDto.Population,
                Buildings = cityDto.Buildings.Select(buildingDto => new BuildingEntity
                {
                    Name = buildingDto.Name,
                    Level = buildingDto.Level
                }).ToList()
            }).ToList()
        };

        _repository.Create(provinceEntity);
    }

    public void Delete(ProvinceDTO province)
    {
        _repository.Delete(new ProvinceEntity
        {
            Name = province.Name
        });
    }

    public List<ProvinceModel> GetAll()
    {
        return _repository.GetAll();
    }

    public int GetIdByName(string name)
    {
        return _repository.GetIdByName(name);
    }

    public void Update(ProvinceDTO province)
    {
        throw new NotImplementedException();
    }
}
