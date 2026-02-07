namespace WebBattler.Services.Mappers;

public interface IEntityMapper<TModel, TDomain>
    where TModel : class
{
    public TModel ToModel(TDomain domain, TModel model = null);
    public TDomain ToDomain(TModel entity);
}
