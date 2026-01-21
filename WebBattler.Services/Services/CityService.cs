using WebBattler.DAL.DTO;
using WebBattler.DAL.Models;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Services;

public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;

    public CityService(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    public void Create(CityDTO city)
    {
        CityEntity cityEntity = new CityEntity
        {
            Name = city.Name,
            Population = city.Population,
            Level = city.Level
        };

        _cityRepository.Create(cityEntity);
    }

    public void Delete(CityDTO city)
    {
        _cityRepository.Delete(new CityEntity
        {
            Name = city.Name,
            Population = city.Population,
            Level = city.Level
        });
    }

    public List<CityModel> GetAll()
    {
        return _cityRepository.GetAll();
    }

    public void Update(CityDTO city)
    {
        throw new NotImplementedException();
    }
}
