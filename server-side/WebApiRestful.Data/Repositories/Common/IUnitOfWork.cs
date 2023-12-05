namespace WebApiRestful.Data.Repositories.Component
{
    public interface IUnitOfWork
    {
        ITodoRepository Todos { get; }
        IUserRepository Users { get; }
    }
}
