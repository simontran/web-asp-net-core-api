namespace TodoWebApiRestful.Common.PersistenceLayer.Repositories
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
