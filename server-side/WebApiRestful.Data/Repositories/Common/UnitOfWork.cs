namespace WebApiRestful.Data.Repositories.Component
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ITodoRepository todoRepository)
        {
            Todos = todoRepository;
        }
        public ITodoRepository Todos { get; }
    }
}
