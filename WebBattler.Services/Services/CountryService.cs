using WebBattler.DAL.DTO;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using WebBattler.DAL.Models;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Services;

public class CountryService : ICountryService
{
    private readonly ICountryRepository _countryRepository;

    public CountryService(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public void Create(CountryDTO country)
    {
        CountryEntity countryEntity = new CountryEntity
        {
            Name = country.Name,
            Provinces = country.Provinces.Select(p => new ProvinceEntity
            {
                Name = p.Name,
                Cities = p.Cities.Select(c => new CityEntity
                {
                    Name = c.Name,
                    Population = c.Population,
                    Level = c.Level
                }).ToList()
            }).ToList()
        };

        _countryRepository.Create(countryEntity);
    }

    public void Delete(CountryDTO country)
    {
        _countryRepository.Delete(new CountryEntity
        {
            Name = country.Name
        });
    }

    public List<CountryModel> GetAll()
    {
        return _countryRepository.GetAll();
    }

    public void Update(CountryDTO country)
    {
        throw new NotImplementedException();
    }
}
