using WebBattler.DAL.DTO;
using WebBattler.DAL.Models;
using WebBattler.DAL.Entities;
using WebBattler.DAL.Interfaces;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Services;

public class CountryService : ICountryService
{
    private readonly ICountryRepository _repository;

    public CountryService(ICountryRepository countryRepository)
    {
        _repository = countryRepository;
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

        _repository.Create(countryEntity);
    }

    public void Delete(CountryDTO country)
    {
        _repository.Delete(new CountryEntity
        {
            Name = country.Name
        });
    }

    public List<CountryModel> GetAll()
    {
        return _repository.GetAll();
    }

    public int GetIdByName(string name)
    {
        return _repository.GetIdByName(name);
    }

    public void Update(CountryDTO country)
    {
        throw new NotImplementedException();
    }
}
