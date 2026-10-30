using ForestGuardian.Domain.Entities;
using ForestGuardian.Domain.Interfaces.Infra;
using ForestGuardian.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ForestGuardian.Infra.Repository;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly ForestGuardianContext _context;
    protected DbSet<T> _dbSet;

    public BaseRepository(ForestGuardianContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<T>();
    }

    public virtual void Alterar(T entity) => _dbSet.Update(entity);

    public async Task<T> Buscar(Expression<Func<T, bool>> predicate)
        => await _dbSet.FirstOrDefaultAsync(predicate);

    public virtual async Task<List<T>> BuscarVarios(Expression<Func<T, bool>> predicate)
        => await _dbSet.Where(predicate).ToListAsync();

    public virtual void Incluir(T entity)
    {
        entity.DataCriacao = DateTime.Now;
        _dbSet.Add(entity);
    }

    public virtual void Remover(T entity) => _dbSet.Remove(entity);

    public virtual async Task<T> SelecionarPorId(Guid id)
        => await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

    public virtual async Task<List<T>> SelecionarTudo()
        => await _dbSet.ToListAsync();

    public void Dispose() => _context.Dispose();

    public async Task<int> SaveChanges() => await _context.SaveChangesAsync();
}