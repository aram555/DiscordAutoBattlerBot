namespace WebBattler.Services.Mappers;

public abstract class EntityMapperBase<TEntity, TDomain>
    : IEntityMapper<TEntity, TDomain>
    where TEntity : class, new()
{
    public TEntity ToEntity(TDomain domain, TEntity entity = null)
    {
        entity ??= new TEntity();
        MapToEntity(domain, entity);

        return entity;
    }

    public TDomain ToDomain(TEntity entity)
    {
        return MapToDomain(entity);
    }

    protected abstract void MapToEntity(TDomain domain, TEntity entity);
    protected abstract TDomain MapToDomain(TEntity entity);
}