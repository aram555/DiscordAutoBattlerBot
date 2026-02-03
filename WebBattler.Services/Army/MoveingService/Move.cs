using WebBattler.DAL.Entities;
using WebBattler.Services.Services;

namespace WebBattler.Services.Army.MoveingService;

public class Move
{
    private readonly ProvinceService _service;

    public Move(ProvinceService service)
    {
        _service = service; 
    }

    public MoveResult MoveToProvince(ArmyEntity army, string provinceName)
    {
        var provinceList = _service.GetAll(army.OwnerId);

        var province = provinceList.FirstOrDefault(p => p.Name == provinceName);

        if (province == null)
        {
            return new MoveResult(false, $"Провинции {provinceName} не существует");
        }

        int id = _service.GetIdByName(provinceName);
        army.ProvinceId = id;

        return new MoveResult(true, "Succes", id);
    }
}
