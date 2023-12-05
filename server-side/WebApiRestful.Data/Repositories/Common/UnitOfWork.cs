namespace WebApiRestful.Data.Repositories.Component
{
    public class UnitOfWork(
        ITodoRepository todoRepository,
        IUserRepository userRepository
        ) : IUnitOfWork
    {
        public ITodoRepository Todos { get; } = todoRepository;
        public IUserRepository Users { get; } = userRepository;

    }
}
