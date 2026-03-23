using AspMarketplace.Web.Data;
using AspMarketplace.Web.Interfaces;
using AspMarketplace.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AspMarketplace.Web.Repositories;

public class Repository<T>(AppDbContext context) : IRepository<T> where T : BaseEntity
{
    private readonly DbSet<T> _set = context.Set<T>();

    public async Task<T?> GetByIdAsync(Guid id)
        => await _set.FirstOrDefaultAsync(e => e.Id == id);

    public async Task<IEnumerable<T>> GetAllAsync()
        => await _set.ToListAsync();

    public IQueryable<T> Query()
        => _set.AsQueryable();

    public IQueryable<T> QueryWithDeleted()
        => _set.IgnoreQueryFilters().AsQueryable();

    public async Task AddAsync(T entity)
        => await _set.AddAsync(entity);

    public void Update(T entity)
        => _set.Update(entity);

    public void SoftDelete(T entity, string deletedBy)
    {
        entity.IsDeleted = true;
        entity.DeletedAt = DateTime.UtcNow;
        entity.DeletedBy = deletedBy;
        _set.Update(entity);
    }

    public void HardDelete(T entity)
        => _set.Remove(entity);

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        => await _set.AnyAsync(predicate);

    public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        => predicate is null
            ? await _set.CountAsync()
            : await _set.CountAsync(predicate);
}
