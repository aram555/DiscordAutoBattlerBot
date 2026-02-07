using WebBattler.DAL.Basis;
using WebBattler.DAL.Models;
using WebBattler.Services.Mappers;

namespace WebBattler.Services.Fabrics;

public class ArmyFabric
{
    public WebBattler.DAL.Basis.Army BuildTree(ArmyModel root)
    {
        return Build(root);
    }

    public WebBattler.DAL.Basis.Army Build(ArmyModel army, WebBattler.DAL.Basis.Army? armyParent = null)
    {
        var domainArmy = new WebBattler.DAL.Basis.Army(
            army.Name,
            new CountryMapper().ToDomain(army.Country),
            new ArmyLocation
            {
                Country = new CountryMapper().ToDomain(army.Country),
                Province = new ProvinceMapper().ToDomain(army.Province),
                City = army.City != null ? new CityMapper().ToDomain(army.City) : null
            },
            armyParent
        );

        if( armyParent != null )
        {
            armyParent.AddSubArmy(domainArmy);
        }

        if(army.Units != null)
        {
            foreach (var unit in army.Units)
            {
                domainArmy.AddUnit(new UnitMapper().ToDomain(unit));
            }
        }

        foreach (var subArmy in army.SubArmies)
        {
            Build(subArmy, domainArmy);
        }

        return domainArmy;
    }




}
