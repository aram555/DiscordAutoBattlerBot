namespace WebBattler.Services.Mappers;

public interface IEntityMapper<TEntity, TDomain>
    where TEntity : class
{
    public TEntity ToEntity(TDomain domain, TEntity entity = null);
    public TDomain ToDomain(TEntity entity);
}
