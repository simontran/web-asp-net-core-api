namespace TodoWebApiRestful.Common.PersistenceLayer.Repositories
{
    public interface IUnitOfWork
    {
        ITodoRepository Todos { get; }
    }
}
