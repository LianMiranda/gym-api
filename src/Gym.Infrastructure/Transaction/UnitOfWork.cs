using Gym.Domain.Interfaces.Repositories;
using Gym.Domain.Interfaces.UnitOfWork;
using Gym.Infrastructure.Database;
using Gym.Infrastructure.Database.Context;
using Gym.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Gym.Infrastructure.Transaction;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IUserRepository? _userRepository;
    private IDbContextTransaction? _transaction;
    private bool _disposed = false;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IUserRepository Users => _userRepository ??= new UserRepository(_context);

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        if (_transaction != null)
            throw new InvalidOperationException("A transaction is already underway.");

        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction == null)
            throw new InvalidOperationException("No transaction has been initiated.");

        try
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction == null)
            return;

        try
        {
            await _transaction.RollbackAsync();
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        // Verifica se já não foi liberado antes
        if (!_disposed)
        {
            // Se chamado via Dispose() explícito
            if (disposing)
            {
                // Libera recursos GERENCIADOS
                _transaction?.Dispose();
                _context.Dispose();
            }

            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true); // Libera recursos gerenciados
        GC.SuppressFinalize(this); // Diz ao GC: "não precisa chamar finalizer"
    }
}