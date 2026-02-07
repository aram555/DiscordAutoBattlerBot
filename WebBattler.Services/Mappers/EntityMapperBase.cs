namespace WebBattler.Services.Mappers;

public abstract class EntityMapperBase<TModel, TDomain>
    : IEntityMapper<TModel, TDomain>
    where TModel : class, new()
{
    public TModel ToModel(TDomain domain, TModel model = null)
    {
        model ??= new TModel();
        MapToModel(domain, model);

        return model;
    }

    public TDomain ToDomain(TModel model)
    {
        return MapToDomain(model);
    }

    protected abstract void MapToModel(TDomain domain, TModel model);
    protected abstract TDomain MapToDomain(TModel model);
}