namespace TreeDomainLibrary.Repositories
{
    public interface IUnitOfWork
    {
        IEmployeeRepository EmployeeRepository { get; }

        void BeginTransactionAsync();

        Task RollbackAsync();

        Task CompleteAsync();

        Task CommitAsync();
    }
}
