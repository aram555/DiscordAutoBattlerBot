using WebBattler.DAL.DTO;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using WebBattler.DAL.Models;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Services;

public class ProvinceService : IProvinceService
{
    private readonly IProvinceRepository _provinceRepository;

    public ProvinceService(IProvinceRepository provinceRepository)
    {
        _provinceRepository = provinceRepository;
    }

    public void Create(ProvinceDTO province)
    {
        ProvinceEntity provinceEntity = new ProvinceEntity
        {
            Name = province.Name,
            Cities = province.Cities.Select(cityDto => new CityEntity
            {
                Name = cityDto.Name,
                Population = cityDto.Population
            }).ToList()
        };

        _provinceRepository.Create(provinceEntity);
    }

    public void Delete(ProvinceDTO province)
    {
        _provinceRepository.Delete(new ProvinceEntity
        {
            Name = province.Name
        });
    }

    public List<ProvinceModel> GetAll()
    {
        return _provinceRepository.GetAll();
    }

    public void Update(ProvinceDTO province)
    {
        throw new NotImplementedException();
    }
}
