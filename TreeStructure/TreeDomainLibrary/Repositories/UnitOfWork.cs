using Microsoft.EntityFrameworkCore.Storage;
using TreeDomainLibrary.Models;

namespace TreeDomainLibrary.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OfficeContext _context;
        private IDbContextTransaction? _transaction;

        public IEmployeeRepository EmployeeRepository { get; private set; }

        public UnitOfWork(OfficeContext context)
        {
            _context = context;
            EmployeeRepository = new EmployeeRepository(_context);
        }

        public async void BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                _transaction = null;
            }
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                _transaction = null;
            }
        }

    }
}
